using Telegram.Bot;
using Telegram.BotBuilder.Models;

namespace Telegram.BotBuilder.CalendarPicker.Bots
{
    public class CalendarBot : BotBase
    {
        public CalendarBot(string username, ITelegramBotClient client) : base(username, client)
        {
        }

        public CalendarBot(string username, string token) : base(username, token)
        {
        }
    }
}
