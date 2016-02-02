using System.Web;
using System.Web.Mvc;

namespace Elliptical.Mvc
{
    public class ActiveMenuItemActionResult : ActionResult
    {
        private ActionResult _innerResult { get; set; }
        private string _item { get; set; }
       

        public ActiveMenuItemActionResult(ActionResult innerResult, string item)
        {
            _innerResult = innerResult;
            _item = item;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.ViewData[_item] = "active";
            _innerResult.ExecuteResult(context);
        }
    }
}