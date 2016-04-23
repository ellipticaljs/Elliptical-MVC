using System;
using System.Web;

namespace Elliptical.Mvc
{
    public partial class HtmlHelpers
    {
        public IHtmlString MetaCharSet()
        {
            string tag = "<meta charset=\"utf - 8\" />";
            return new HtmlString(tag);
        }

        public IHtmlString MetaViewport()
        {
            string tag = " <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable = 0\">";
            return new HtmlString(tag);
        }

        public IHtmlString MetaWebApp()
        {
            string tag = "<meta name=\"apple-mobile-web-app-capable\" content=\"yes\" />";
            tag += Environment.NewLine + "<meta name=\"mobile-web-app-capable\" content=\"yes\" />";
            return new HtmlString(tag);
        }
    }
}
