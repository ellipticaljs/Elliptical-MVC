using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elliptical.Mvc
{
    public static class Settings
    {
        public static string VulcanizedLink
        {
            get
            {
                var result = ConfigurationManager.AppSettings["Elliptical.Imports.Link"];
                if (!string.IsNullOrEmpty(result)) return result;
                return "/Content/imports/import.html";
            }
        }

    }
}
