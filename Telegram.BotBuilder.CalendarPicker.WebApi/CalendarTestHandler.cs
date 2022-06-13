using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.BotBuilder.CalendarPicker.Interfaces;

namespace Telegram.BotBuilder.CalendarPicker.WebApi
{
    public class CalendarTestHandler : ICalendarHandler
    {
        public async Task HandlePickedDateAsync(
            ITelegramBotClient context, 
            Message message, 
            DateTime pickedDate, 
            CancellationToken cancellationToken
            )
        {
            await context.SendTextMessageAsync(
                message.Chat, 
                $"PickedDate: {pickedDate:d}", 
                cancellationToken: cancellationToken
                );
        }
    }
}
