using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Telegram.BotBuilder.CalendarPicker.Bots;
using Telegram.BotBuilder.CalendarPicker.Handlers.Calendar;
using Telegram.BotBuilder.CalendarPicker.Handlers.Commands;
using Telegram.BotBuilder.CalendarPicker.Handlers.MonthYear;
using Telegram.BotBuilder.Extensions;
using Telegram.BotBuilder.Interfaces;

namespace Telegram.BotBuilder.CalendarPicker.Extensions
{
    public static class UseCalendarExtension
    {
        public static Task UseCalendarPickerWebhook(
            this IApplicationBuilder app,
            string command,
            Uri uri
        )
        {
            return app.UseTelegramBotWebhook<CalendarBot>(ConfigureCalendarBot(command), uri);
        }

        public static Task UseCalendarPickerLongPolling(
            this IApplicationBuilder app,
            string command,
            TimeSpan startAfter = default,
            CancellationToken cancellationToken = default)
        {
            return app.UseTelegramBotLongPolling<CalendarBot>(ConfigureCalendarBot(command), startAfter, cancellationToken);
        }

        private static IBotBuilder ConfigureCalendarBot(
            string command
        )
        {
            return new BotBuilder()
                .UseCommand<CalendarCommand>(command)
                .UseWhen<ChangeToHandler>(ChangeToHandler.CanHandle)
                .UseWhen<PickDateHandler>(PickDateHandler.CanHandle)
                .UseWhen<ChangeYearToHandler>(ChangeYearToHandler.CanHandle)
                .UseWhen<YearMonthPickerHandler>(YearMonthPickerHandler.CanHandle)
                .UseWhen<MonthPickerHandler>(MonthPickerHandler.CanHandle)
                .UseWhen<YearPickerHandler>(YearPickerHandler.CanHandle);
        }
    }
}
