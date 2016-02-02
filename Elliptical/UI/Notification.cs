

namespace Elliptical.Mvc
{
    public class Notification
    {
        public string CssClass { get; set; }
		public string Message { get; set; }
        public NotificationType Type { get; set; }

		public Notification(string cssClass, string message, NotificationType type )
		{
			CssClass = cssClass;
			Message = message;
            Type = type;
		}
    }
}