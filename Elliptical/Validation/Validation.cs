using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Elliptical.Mvc
{
    public class DataAnnotationsValidator
    {
        public DataAnnotationsValidator(bool camelCaseSerialize)
        {
            camelCase = camelCaseSerialize;
        }

        private bool camelCase { get; set; }

        public Task<ModelValidationSummary> TryValidate(JObject obj, string type)
        {

            return Task.Run(() =>
            {
                var mvSummary = new ModelValidationSummary();
                var summary = new List<string>();
                var classType = qualifiedModelType(type);
                var t = Type.GetType(classType);
                var model = obj.ToObject(t);
                var context = new ValidationContext(model, null, null);
                var results = new List<ValidationResult>();
                mvSummary.IsValid = Validator.TryValidateObject(model, context, results, true);
                if (!mvSummary.IsValid)
                {
                    foreach (var validationResult in results)
                    {
                        var err = validationResult.ErrorMessage;
                        summary.Add(err);
                        var props = validationResult.MemberNames;
                        foreach (var p in props)
                        {
                            var p1 = (camelCase) ? p.ToCamelCaseFromProperCase() : p;
                            obj[p1 + "_placeholder"] = err;
                            obj[p1 + "_error"] = "error";

                        }
                    }
                }

                mvSummary.Model = obj;
                mvSummary.Summary = summary;

                return mvSummary;

            });
            
        }

        public ModelValidationSummary ReturnInternalError(JObject obj)
        {
            var mvSummary = new ModelValidationSummary();
            var summary = new List<string>();
            string err = "Internal server error processing validation";
            summary.Add(err);
            mvSummary.Model = obj;
            mvSummary.Summary = summary;
            return mvSummary;
        }

        private string qualifiedModelType(string type)
        {
            //if period in the type string, don't namespace qualify
            if (type.IndexOf(".") > 0)
            {
                return type;
            }
            //qualify namespace with web.config setting
            var nameSpace = ConfigurationManager.AppSettings["Model.Namespace"];
            return (nameSpace != null) ? nameSpace + "." + type : type;
        }
    }

   
}