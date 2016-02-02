using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Elliptical.Mvc
{
    public class ModelValidation
    {
        public JObject Model { get; set; }
        public string Type { get; set; }
    }

    public class ModelValidationSummary
    {
        public ModelValidationSummary()
        {
            IsValid = true;
            Summary = new List<string>();
        }

        public bool IsValid { get; set; }
        public JObject Model { get; set; }
        public IEnumerable<string> Summary { get; set; }
    }
}