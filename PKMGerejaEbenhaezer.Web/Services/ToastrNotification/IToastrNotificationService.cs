namespace PKMGerejaEbenhaezer.Web.Services.ToastrNotification
{
    public interface IToastrNotificationService
    {
        string? GetNotificationJson();
        void AddNotification(ToastrNotification notification);
    }
}
