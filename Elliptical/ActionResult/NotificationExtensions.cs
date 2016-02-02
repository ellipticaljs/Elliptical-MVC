using System.Collections.Generic;
using System.Web.Mvc;

namespace Elliptical.Mvc
{
    public static class NotificationExtensions
    {
        private const string Notifications = "_Notifications";

        public static List<Notification> GetNotifications(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(Notifications))
            {
                tempData[Notifications] = new List<Notification>();
            }

            return (List<Notification>) tempData[Notifications];
        }

        public static ActionResult WithSuccessNotification(this ActionResult result, string message)
        {
            return new NotificationActionResult(result, new Notification("success",message,NotificationType.Success));
        }

        public static ActionResult WithInfoNotification(this ActionResult result, string message)
        {
            return new NotificationActionResult(result, new Notification("info", message, NotificationType.Info));
        }

        public static ActionResult WithWarninNotificationg(this ActionResult result, string message)
        {
            return new NotificationActionResult(result, new Notification("warning", message, NotificationType.Warning));
        }

        public static ActionResult WithErrorNotification(this ActionResult result, string message)
        {
            return new NotificationActionResult(result, new Notification("error", message, NotificationType.Error));
        }

        public static ActionResult WithNotification(this ActionResult result, string message)
        {
            return new NotificationActionResult(result, new Notification("none", message, NotificationType.Toast));
        }
    }
}