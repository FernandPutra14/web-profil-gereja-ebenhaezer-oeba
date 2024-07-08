namespace PKMGerejaEbenhaezer.Web.Services.ToastrNotification
{
    public class ToastrNotification
    {
        public ToastrNotificationType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
    }

    public enum ToastrNotificationType
    {
        Success, Warning, Error, Info
    }
}
