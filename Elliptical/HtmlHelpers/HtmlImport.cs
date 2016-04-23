using System;
using System.Web;

namespace Elliptical.Mvc
{
    public partial class HtmlHelpers
    {
        public IHtmlString Import(string url)
        {
            string link = "<link rel=\"import\" href=\"" + url + "\"/>";
            return new HtmlString(link);
        }

        public IHtmlString VulcanizedImport()
        {
            string url = Settings.VulcanizedLink;
            string link = "<link rel=\"import\" href=\"" + url + "\"/>";
            return new HtmlString(link);
        }

        public IHtmlString WebComponentsLite()
        {
            string script = "<script src=\"/Content/components/webcomponentsjs/webcomponents-lite.js\"></script>";
            return new HtmlString(script);
        }

        public IHtmlString WebComponents()
        {
            string script = "<script src=\"/Content/components/webcomponentsjs/webcomponents.js\"></script>";
            return new HtmlString(script);
        }
    }
}
