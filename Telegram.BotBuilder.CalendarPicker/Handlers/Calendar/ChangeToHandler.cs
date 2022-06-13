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
    public class ChangeToHandler : IUpdateHandler
    {
        private readonly LocalizationService _locale;

        public ChangeToHandler(LocalizationService locale)
        {
            _locale = locale;
        }

        public static bool CanHandle(IUpdateContext update)
        {
            return
                update.Update.Type == UpdateType.CallbackQuery
                &&
                update.Update.IsCallbackCommand(Constants.ChangeTo);
        }

        public async Task HandleUpdateAsync(
            ITelegramBotClient context, 
            Update update,
            CancellationToken cancellationToken
            )
        {
            if (!DateTime.TryParseExact(
                    update.TrimCallbackCommand(Constants.ChangeTo),
                    Constants.DateFormat,
                    null,
                    DateTimeStyles.None,
                    out var date)
               )
            {
                return;
            }

            var calendarMarkup = MarkupHelper.Calendar(date, _locale.DateCulture);

            await context.EditMessageReplyMarkupAsync(
                update.CallbackQuery.Message.Chat.Id,
                update.CallbackQuery.Message.MessageId,
                replyMarkup: calendarMarkup,
                cancellationToken: cancellationToken
            );
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw exception;
        }
    }
}
