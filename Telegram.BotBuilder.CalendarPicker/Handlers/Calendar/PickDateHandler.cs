using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.BotBuilder.CalendarPicker.Interfaces;
using Telegram.BotBuilder.CalendarPicker.Services;
using Telegram.BotBuilder.Extensions;
using Telegram.BotBuilder.Interfaces;

namespace Telegram.BotBuilder.CalendarPicker.Handlers.Calendar
{
    public class PickDateHandler : IUpdateHandler
    {
        private readonly LocalizationService _locale;
        private readonly ISendDateHandler _sendDateHandler;

        public PickDateHandler(LocalizationService locale
            , ISendDateHandler sendDateHandler
            )
        {
            _locale = locale;
            _sendDateHandler = sendDateHandler;
        }

        public static bool CanHandle(IUpdateContext context)
        {
            return
                context.Update.Type == UpdateType.CallbackQuery
                &&
                context.Update.IsCallbackCommand(Constants.PickDate);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParseExact(
                    update.TrimCallbackCommand(Constants.PickDate),
                    Constants.DateFormat,
                    null,
                    DateTimeStyles.None,
                    out var date)
               )
            {
                return;
            }

            //await botClient.EditMessageTextAsync(
            //    update.CallbackQuery.Message.Chat.Id,
            //    update.CallbackQuery.Message.MessageId,
            //    $"PickedDate: {date:d}"
            //);
            
            await _sendDateHandler.HandlePickedDateAsync(botClient, update.CallbackQuery.Message, date, cancellationToken);
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw exception;
        }
    }
}
