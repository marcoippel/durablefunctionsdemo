using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DurableFunctionDemo.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DurableFunctionDemo
{
    public static class WineFunction
    {
        [FunctionName("WineFunction")]
        public static async Task Run(
            [BlobTrigger("wine/{name}")]Stream myBlob,
            string name,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            log.LogDebug($"Process file: {name}");
            await starter.StartNewAsync("O_Orchestrator", name);
        }

        [FunctionName("O_Orchestrator")]
        public static async Task Orchestrator([OrchestrationTrigger] DurableOrchestrationContext context, ILogger log)
        {
            var fileName = context.GetInput<string>();

            var wineCountries = await context.CallActivityAsync<Countries[]>("A_ExtractWineCountries", fileName);

            var tasks = new List<Task<Wine[]>>();
            foreach (Countries wineCountry in wineCountries)
            {
                tasks.Add(context.CallActivityAsync<Wine[]>("A_ExtractWines", wineCountry));
            }

            var wineTasks = await Task.WhenAll(tasks);

            List<Task> uploadWineTasks = new List<Task>();
            foreach (Wine wineTask in wineTasks.SelectMany(x => x))
            {
                uploadWineTasks.Add(context.CallActivityAsync("A_UploadWine", wineTask));
            }

            await Task.WhenAll(uploadWineTasks);

            if (!context.IsReplaying)
            {
                log.LogDebug($"Finished file: {fileName}");
            }
        }

        [FunctionName("A_ExtractWineCountries")]
        public static async Task<Countries[]> ExtractWineCountries([ActivityTrigger] string name, IBinder binder)
        {
            var blob = await binder.BindAsync<CloudBlob>(new BlobAttribute($"wine/{name}"));
            Countries[] wineData = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(memoryStream);
                memoryStream.Position = 0;
                wineData = memoryStream.DeserializeFromXml<Countries[]>();
            }

            return wineData;
        }

        [FunctionName("A_ExtractWines")]
        public static Wine[] ExtractWines([ActivityTrigger] Countries country)
        {
            return country.Wines;
        }

        [FunctionName("A_UploadWine")]
        public static async Task UploadWine(
            [ActivityTrigger] Wine wine,
            IBinder binder,
            ILogger log)
        {

            using (var writer = await binder.BindAsync<TextWriter>(new BlobAttribute($"wines/{wine.Id}.json")))
            {
                writer.Write(JsonConvert.SerializeObject(wine));
            }
        }
    }
}
