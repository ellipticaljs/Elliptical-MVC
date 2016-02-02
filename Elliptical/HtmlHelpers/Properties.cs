using System;
using System.Web.Mvc;


namespace Elliptical.Mvc
{
    public partial class HtmlHelpers
    {
        public HtmlHelper helper { get; set; }

        /// <summary>
        /// Constructor for the Elliptical HtmlHelpers class
        /// </summary>
        /// <param name="helper"></param>
        public HtmlHelpers(HtmlHelper helper)
        {
            this.helper = helper;
        }
    }
}