using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace PKMGerejaEbenhaezer.Web.Services.ToastrNotification
{
    public class ToastrNotificationService : IToastrNotificationService
    {
        private readonly string _tempDataKey = "ToastrTempData";

        private readonly ITempDataDictionary _tempDataDictionary;

        public ToastrNotificationService(IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _tempDataDictionary = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
        }

        public void AddNotification(ToastrNotification notification)
        {
            var json = JsonConvert.SerializeObject(notification, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
            });
            _tempDataDictionary[_tempDataKey] = json;
        }

        public string? GetNotificationJson()
        {
            var notification = _tempDataDictionary[_tempDataKey];

            return notification is not null && notification is string json ? json : null;
        }
    }
}
