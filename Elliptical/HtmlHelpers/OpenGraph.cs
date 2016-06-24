using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Elliptical.Mvc
{
    public partial class HtmlHelpers
    {
        public IHtmlString MetaOpenGraph(Dictionary<string,string> graph)
        {
            if (graph == null) return null;

            string meta = "";
            foreach(var item in graph)
            {
                meta += "<meta property=\"og:" + item.Key + "\" content=\"" + item.Value + "\" />" + Environment.NewLine;
            }
            return new HtmlString(meta);
        }
    }
}
