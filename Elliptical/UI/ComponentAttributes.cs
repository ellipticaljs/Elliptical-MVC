using System.Collections.Generic;

namespace Elliptical.Mvc
{
    public class FormAttributes : ElementAttributes
    {
        public FormAttributes()
        {
            Method = HtmlFormMethod.Post;
            Action = null;
            IncludeFormTag = false;
            ReturnUrl = null;
           
        }

        public string Schema { get; set; }
        public HtmlFormMethod Method { get; set; }
        public string Action { get; set; }
        public bool IncludeFormTag { get; set; }
        public string ReturnUrl { get; set; }
        public string ProcessingMessage { get; set; }
        public string SuccessMessage { get; set; }
        
    }

    public class ElementAttributes : ComponentAttributes
    {
        public ElementAttributes()
        {
            CssClass = null;
            Placeholder = null;
            Required = false;
            ReadOnly = false;
            Enabled = true;
            EABind = null;
            Attributes = new Dictionary<string, string>();
        }

        public string CssClass { get; set; }
        public string Placeholder { get; set; }
        public bool Required { get; set; }
        public bool ReadOnly { get; set; }
        public bool Enabled { get; set; }
        public string Value { get; set; }
        public string EABind { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
    }

    public class ComponentAttributes
    {
        public ComponentAttributes()
        {
            Scope = null;
            Id = null;
            Name = null;
            Service = null;
            ImportHref = null;
            Preload = false;
            DataUpgraded = false;
            Role = null;
            Channel = null;
            Event = null;
            EnableClientBinding = true;
            
        }

        public string Scope { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Service { get; set; }
        public string ImportHref { get; set; }
        public string Label { get; set; }
        public string Message { get; set; }
        public string ComponentCss { get; set; }
        public bool Preload { get; set; }
        public bool DataUpgraded { get; set; }
        public string Role { get; set; }
        public string Channel { get; set; }
        public string Event { get; set; }
        public bool EnableClientBinding { get; set; }
    }
}