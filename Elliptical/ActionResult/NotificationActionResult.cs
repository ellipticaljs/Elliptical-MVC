using System.Web.Mvc;

namespace Elliptical.Mvc
{
    public class NotificationActionResult : ActionResult
    {
        private ActionResult _innerResult;
        private string _cssClass { get; set; }
        private string _message { get; set; }
        private NotificationType _type { get; set; }

        public NotificationActionResult(ActionResult innerResult, Notification notification)
        {
            _innerResult = innerResult;
            _cssClass = notification.CssClass;
            _message = notification.Message;
            _type = notification.Type;
        }

        

        public override void ExecuteResult(ControllerContext context)
        {
            var alerts = context.Controller.TempData.GetNotifications();
            alerts.Add(new Notification(_cssClass, _message,_type));
            _innerResult.ExecuteResult(context);
        }
    }
}