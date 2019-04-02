using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DurableFunctionDemo
{
    public static class Utility
    {
        public static T DeserializeFromXml<T>(this Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        public static string ToXml(this object value)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());

            using(StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, value);
                return textWriter.ToString();
            }
        }
    }
}
