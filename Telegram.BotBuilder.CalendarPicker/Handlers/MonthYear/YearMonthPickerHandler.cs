using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.BotBuilder.CalendarPicker.Helpers;
using Telegram.BotBuilder.CalendarPicker.Services;
using Telegram.BotBuilder.Extensions;
using Telegram.BotBuilder.Interfaces;

namespace Telegram.BotBuilder.CalendarPicker.Handlers.MonthYear
{
    public class YearMonthPickerHandler : IUpdateHandler
    {
        private readonly LocalizationService _locale;

        public YearMonthPickerHandler(LocalizationService locale)
        {
            _locale = locale;
        }

        public static bool CanHandle(IUpdateContext context)
        {
            return
                context.Update.Type == UpdateType.CallbackQuery
                &&
                context.Update.IsCallbackCommand(Constants.YearMonthPicker);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParseExact(
                    update.TrimCallbackCommand(Constants.YearMonthPicker),
                    Constants.DateFormat,
                    null,
                    DateTimeStyles.None,
                    out var date)
               )
            {
                return;
            }

            var monthYearMarkup = MarkupHelper.PickMonthYear(date, _locale.DateCulture);

            await botClient.EditMessageReplyMarkupAsync(
                update.CallbackQuery.Message.Chat.Id,
                update.CallbackQuery.Message.MessageId,
                replyMarkup: monthYearMarkup,
                cancellationToken: cancellationToken
            );
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw exception;
        }
    }
}
