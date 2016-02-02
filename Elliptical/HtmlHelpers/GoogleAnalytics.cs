using System;
using System.Web;
using System.Web.Mvc;


namespace Elliptical.Mvc
{
    public partial class HtmlHelpers
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="uaId"></param>
        /// <returns></returns>
        public IHtmlString GoogleAnalytics(string uaId)
        {
            string s = "<script>" + Environment.NewLine;
            s += "(function (i, s, o, g, r, a, m) {" + Environment.NewLine;
            s += "i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {" + Environment.NewLine;
            s += "(i[r].q = i[r].q || []).push(arguments)" + Environment.NewLine;
            s += " }, i[r].l = 1 * new Date(); a = s.createElement(o)," + Environment.NewLine;
            s += " m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)" + Environment.NewLine;
            s += "})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');" + Environment.NewLine;
            s += "ga('create', '" + uaId + "', 'auto');" + Environment.NewLine;
            s += "ga('send', 'pageview');" + Environment.NewLine;
            s += "</script>" + Environment.NewLine;

            return new MvcHtmlString(s);
        }
    }
}