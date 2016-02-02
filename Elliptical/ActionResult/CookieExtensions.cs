using System.Collections.Generic;
using System.Web.Mvc;
namespace Elliptical.Mvc
{
    public static class CookieExtensions
    {
        
        public static ActionResult WithCookie(this ActionResult result, string name,string value)
        {
            return new CookieActionResult<string>(result,name,value);
        }

        public static ActionResult WithCookie<T>(this ActionResult result, string name, T value)
        {
            return new CookieActionResult<T>(result, name, value);
        }

       
    }
}