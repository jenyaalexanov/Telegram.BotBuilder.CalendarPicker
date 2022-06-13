using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram.BotBuilder.CalendarPicker.Interfaces
{
    public interface ICalendarHandler
    {
        Task HandlePickedDateAsync(
            ITelegramBotClient context,
            Message message,
            DateTime pickedDate,
            CancellationToken cancellationToken
            );
    }
}
