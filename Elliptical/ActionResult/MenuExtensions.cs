using System.Web.Mvc;

namespace Elliptical.Mvc
{
    public static class MenuExtensions
    {
        public static ActionResult WithActiveMenuItem(this ActionResult result, string item)
        {
            return new ActiveMenuItemActionResult(result, item);
        }
    }
}