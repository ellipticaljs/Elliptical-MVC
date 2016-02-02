using System.Web;

namespace Elliptical.Mvc
{
    public partial class HtmlHelpers
    {
        public IHtmlString ActiveMenuItem(string item)
        {
            var objActive = helper.ViewData[item];
            var active = "";
            if (objActive != null)
            {
                active = objActive.ToString();
            }
            return new HtmlString(active);
        }
    }
}