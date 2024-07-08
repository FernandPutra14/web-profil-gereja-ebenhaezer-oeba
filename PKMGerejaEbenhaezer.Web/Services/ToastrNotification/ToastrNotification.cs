using Newtonsoft.Json;

namespace PKMGerejaEbenhaezer.Web.Services.ToastrNotification
{
    public class ToastrNotification
    {
        public ToastrNotificationType Type { get; set; }
        public string? Title { get; set; }
        public string Message { get; set; } = string.Empty;
        public ToastrOptions? Options { get; set; }
    }

    public class ToastrOptions
    {
        public string? positionClass { get; set; }
        public bool? closeButton { get; set; }
        public bool? newestOnTop { get; set; }
        public bool? progressBar { get; set; }
        public bool? preventDuplicates { get; set; }
        public int? showDuration { get; set; }
        public int? hideDuration { get; set; }
        public int? timeOut { get; set; }
        public int? extendedTimeOut { get; set; }
        public string? showMethod { get; set; }
        public string? hideMethod { get; set; }
        public string? showEasing { get; set; }
        public string? hideEasing { get; set; }
    }

    public static class AnimationMethods
    {
        public const string FadeIn = "fadeIn";
        public const string FadeOut = "fadeOut";
        public const string SlideUp = "slideUp";
        public const string SlideDown = "slideDown";
        public const string Show = "show";
        public const string Hide = "hide";
    }

    public static class Easings
    {
        public const string Swing = "swing";
        public const string Linear = "linear";
    }

    public static class PositionClasses
    {
        public const string TopRight = "toast-top-right";
        public const string BottomRight = "toast-bottom-right";
        public const string TopLeft = "toast-top-left";
        public const string TopFullWidth = "toast-top-full-width";
        public const string BottomFullWidth = "toast-bottom-full-width";
        public const string TopCenter = "toast-top-center";
        public const string BottomCenter = "toast-bottom-center";
    }

    public enum ToastrNotificationType
    {
        Success, Warning, Error, Info
    }
}
