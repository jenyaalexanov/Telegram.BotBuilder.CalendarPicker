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

namespace Telegram.BotBuilder.CalendarPicker.Handlers.Calendar
{
    public class ChangeYearToHandler : IUpdateHandler
    {
        private readonly LocalizationService _locale;

        public ChangeYearToHandler(LocalizationService locale)
        {
            _locale = locale;
        }

        public static bool CanHandle(IUpdateContext update)
        {
            return
                update.Update.Type == UpdateType.CallbackQuery
                &&
                update.Update.IsCallbackCommand(Constants.ChangeYearTo);
        }

        public async Task HandleUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken
        )
        {
            if (!DateTime.TryParseExact(
                    update.TrimCallbackCommand(Constants.ChangeYearTo),
                    Constants.DateFormat,
                    null,
                    DateTimeStyles.None,
                    out var date)
               )
            {
                return;
            }

            var monthPickerMarkup = MarkupHelper.PickYear(date, _locale.DateCulture);

            await botClient.EditMessageReplyMarkupAsync(
                update.CallbackQuery.Message.Chat.Id,
                update.CallbackQuery.Message.MessageId,
                replyMarkup: monthPickerMarkup
            );
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw exception;
        }
    }
}
