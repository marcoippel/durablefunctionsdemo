namespace DurableFunctionDemo.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Countries
    {

        private string countryField;

        private Wine[] winesField;

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Wine", IsNullable = false)]
        public Wine[] Wines
        {
            get
            {
                return this.winesField;
            }
            set
            {
                this.winesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Wine
    {

        private int _idField;

        private string _countryField;

        private string _descriptionField;

        private string _designationField;

        private byte _pointsField;

        private string _priceField;

        private string _provinceField;

        private string _region1Field;

        private string _region2Field;

        private string _varietyField;

        private string _wineryField;

        /// <remarks/>
        public int Id
        {
            get
            {
                return this._idField;
            }
            set
            {
                this._idField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this._countryField;
            }
            set
            {
                this._countryField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this._descriptionField;
            }
            set
            {
                this._descriptionField = value;
            }
        }

        /// <remarks/>
        public string Designation
        {
            get
            {
                return this._designationField;
            }
            set
            {
                this._designationField = value;
            }
        }

        /// <remarks/>
        public byte Points
        {
            get
            {
                return this._pointsField;
            }
            set
            {
                this._pointsField = value;
            }
        }

        /// <remarks/>
        public string Price
        {
            get
            {
                return this._priceField;
            }
            set
            {
                this._priceField = value;
            }
        }

        /// <remarks/>
        public string Province
        {
            get
            {
                return this._provinceField;
            }
            set
            {
                this._provinceField = value;
            }
        }

        /// <remarks/>
        public string Region1
        {
            get
            {
                return this._region1Field;
            }
            set
            {
                this._region1Field = value;
            }
        }

        /// <remarks/>
        public string Region2
        {
            get
            {
                return this._region2Field;
            }
            set
            {
                this._region2Field = value;
            }
        }

        /// <remarks/>
        public string Variety
        {
            get
            {
                return this._varietyField;
            }
            set
            {
                this._varietyField = value;
            }
        }

        /// <remarks/>
        public string Winery
        {
            get
            {
                return this._wineryField;
            }
            set
            {
                this._wineryField = value;
            }
        }
    }
}