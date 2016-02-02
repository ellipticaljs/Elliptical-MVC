using System.Web.Mvc;

namespace Elliptical.Mvc
{
    public static class Helpers
    {
        // Extension method - allows us to "namespace" Elliptical HtmlHelpers with an Elliptical() method---> @Html.Elliptical().ViewBag
        /// <summary>
        ///     Elliptical HtmlHelpers namespace
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static HtmlHelpers Elliptical(this HtmlHelper helper)
        {
            return new HtmlHelpers(helper);
        }
    }
}