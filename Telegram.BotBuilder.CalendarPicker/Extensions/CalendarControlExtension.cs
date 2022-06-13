using Microsoft.Extensions.DependencyInjection;
using System;
using Telegram.Bot;
using Telegram.BotBuilder.CalendarPicker.Bots;
using Telegram.BotBuilder.CalendarPicker.Handlers;
using Telegram.BotBuilder.CalendarPicker.Handlers.Calendar;
using Telegram.BotBuilder.CalendarPicker.Handlers.Commands;
using Telegram.BotBuilder.CalendarPicker.Handlers.MonthYear;
using Telegram.BotBuilder.CalendarPicker.Interfaces;
using Telegram.BotBuilder.CalendarPicker.Services;

namespace Telegram.BotBuilder.CalendarPicker.Extensions
{
    public static class CalendarControlExtension
    {
        public static IServiceCollection AddCalendarPicker<THandler>(
            this IServiceCollection services,
            string username, 
            ITelegramBotClient client,
            string? locale = null,
            string? datePickerTitle = null,
            DateTime? initialDate = null
            ) where THandler : ICalendarHandler
        {
            return services.AddTransient(_ => new CalendarBot(username, client))
                .RegisterOtherPart<THandler>(locale, datePickerTitle, initialDate);
        }

        public static IServiceCollection AddCalendarPicker<THandler>(
            this IServiceCollection services,
            string username,
            string token,
            string? locale = null,
            string? datePickerTitle = null,
            DateTime? initialDate = null
            ) where THandler : ICalendarHandler
        {
            return services.AddTransient(_ => new CalendarBot(username, token))
                .RegisterOtherPart<THandler>(locale, datePickerTitle, initialDate);
        }

        private static IServiceCollection RegisterOtherPart<THandler>(
            this IServiceCollection services, 
            string? locale,
            string? datePickerTitle,
            DateTime? initialDate
            ) where THandler : ICalendarHandler
        {
            return services.AddScoped(_ =>
                    new CalendarCommand(new LocalizationService(locale), datePickerTitle, initialDate))
                .AddScoped<ChangeToHandler>()
                .AddScoped<PickDateHandler>()
                .AddScoped<YearMonthPickerHandler>()
                .AddScoped<MonthPickerHandler>()
                .AddScoped<YearPickerHandler>()
                .AddScoped<ChangeYearToHandler>()
                .AddTransient(_ => new LocalizationService(locale))
                .AddScoped(typeof(THandler))
                .AddScoped<ISendDateHandler, SendDateHandler<THandler>>();
        }
    }
}
