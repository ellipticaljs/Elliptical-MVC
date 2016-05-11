using System;
using System.Collections.Generic;


namespace Elliptical.Mvc
{
    public class HtmlForm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
        public string Action { get; set; }
        public string SuccessMessage { get; set; }
        public string FailureMessage { get; set; }
        public Dictionary<string,string> ReadOnly { get; set; }
        public Dictionary<string,string> Required { get; set; }
        public string Service { get; set; }
        public string NotifyService { get; set; }
        public string ValidationService { get; set; }
        public string ButtonLabel { get; set; }

        public HtmlForm()
        {
            ReadOnly = new Dictionary<string, string>();
            Required = new Dictionary<string, string>();
        }
    }
}
