using System.Web;
using System.Web.Mvc;
using RevStack.Mvc;

namespace Elliptical.Mvc
{
    public class CookieActionResult<T> : ActionResult
    {
        private ActionResult _innerResult { get; set; }
        private string _name { get; set; }
        private string _value { get; set; }

        public CookieActionResult(ActionResult innerResult, string name, string value)
        {
            _innerResult = innerResult;
            _name = name;
            _value = value;
        }
        
        public CookieActionResult(ActionResult innerResult, string name, T value)
        {
            _innerResult = innerResult;
            _name = name;
            _value = Json.SerializeObject(value,true,true);
        }

      
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Cookies[_name].Value = _value;
            _innerResult.ExecuteResult(context);
        }
    }
}