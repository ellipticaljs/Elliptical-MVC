using System;
using System.Configuration;

namespace Elliptical.Mvc.Configuration
{
    /// <summary>
    /// </summary>
    public class BundleSection : ConfigurationSection
    {
        [ConfigurationProperty("css")]
        public Css Css
        {
            get { return (Css) this["css"]; }
            set { this["css"] = value; }
        }

        [ConfigurationProperty("platform")]
        public Platform Platform
        {
            get { return (Platform) this["platform"]; }
            set { this["platform"] = value; }
        }

        [ConfigurationProperty("framework")]
        public Framework Framework
        {
            get { return (Framework) this["framework"]; }
            set { this["framework"] = value; }
        }
    }

    public class Css : ConfigurationElement
    {
        [ConfigurationProperty("virtualPath")]
        public string VirtualPath
        {
            get { return this["virtualPath"].ToString(); }
            set { this["virtualPath"] = value; }
        }

        [ConfigurationProperty("items")]
        public Items Items
        {
            get { return (Items) this["items"]; }
            set { this["items"] = value; }
        }
    }

    public class Platform : ConfigurationElement
    {
        [ConfigurationProperty("virtualPath")]
        public string VirtualPath
        {
            get { return this["virtualPath"].ToString(); }
            set { this["virtualPath"] = value; }
        }

        [ConfigurationProperty("items")]
        public Items Items
        {
            get { return (Items) this["items"]; }
            set { this["items"] = value; }
        }
    }

    public class Framework : ConfigurationElement
    {
        [ConfigurationProperty("virtualPath")]
        public string VirtualPath
        {
            get { return this["virtualPath"].ToString(); }
            set { this["virtualPath"] = value; }
        }

        [ConfigurationProperty("items")]
        public Items Items
        {
            get { return (Items) this["items"]; }
            set { this["items"] = value; }
        }
    }

    public class Items : ConfigurationElementCollection
    {
        protected override string ElementName
        {
            get { return "add"; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        public Add this[int index]
        {
            get { return BaseGet(index) as Add; }
        }

        public new Add this[string key]
        {
            get { return BaseGet(key) as Add; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Add();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Add) element).Key;
        }

        protected override bool IsElementName(string elementName)
        {
            return !String.IsNullOrEmpty(elementName) && elementName == "add";
        }
    }
}