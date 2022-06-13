using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.BotBuilder.CalendarPicker.Interfaces;

namespace Telegram.BotBuilder.CalendarPicker.Handlers
{
    public class SendDateHandler<THandler> : ISendDateHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public SendDateHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task HandlePickedDateAsync(
            ITelegramBotClient context,
            Message message,
            DateTime pickedDate,
            CancellationToken cancellationToken
            )
        {
            var calendarHandler = (ICalendarHandler)_serviceProvider.GetService(typeof(THandler));
            return calendarHandler.HandlePickedDateAsync(context, message, pickedDate, cancellationToken);
        }
    }
}
