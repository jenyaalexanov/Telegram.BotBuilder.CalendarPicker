using System.Globalization;

namespace Telegram.BotBuilder.CalendarPicker.Services
{
    public class LocalizationService
    {
        public DateTimeFormatInfo DateCulture { get; set; }

        public LocalizationService(string? locale)
        {
            DateCulture = string.IsNullOrEmpty(locale)
                ? new CultureInfo("en-US", false).DateTimeFormat
                : new CultureInfo(locale, false).DateTimeFormat;
        }
    }
}
