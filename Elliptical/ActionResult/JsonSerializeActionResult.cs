using System;
using System.Web.Mvc;


namespace Elliptical.Mvc
{
    public class JsonSerializeViewResult : ViewResult
    {
        private ViewResult _innerResult;
        private string _jsonModel;
        private string _contextProp;
        public JsonSerializeViewResult(ViewResult innerResult, string jsonModel,string contextProp)
        {
            _innerResult = innerResult;
            _jsonModel = jsonModel;
            _contextProp = contextProp;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.TempData.SetViewModel(_jsonModel);
            context.Controller.TempData.SetViewModelProp(_contextProp);
            _innerResult.ExecuteResult(context);
        }
    }
}
