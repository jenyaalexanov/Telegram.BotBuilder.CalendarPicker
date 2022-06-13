using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.BotBuilder.CalendarPicker.Helpers;
using Telegram.BotBuilder.CalendarPicker.Services;
using Telegram.BotBuilder.Models;

namespace Telegram.BotBuilder.CalendarPicker.Handlers.Commands
{
    public class CalendarCommand : CommandBase
    {
        private readonly LocalizationService _localizationService;
        private readonly string? _datePickerTitle;
        private readonly DateTime? _initialDateTime;

        public CalendarCommand(
            LocalizationService localizationService,
            string? datePickerTitle,
            DateTime? initialDateTime
            )
        {
            _localizationService = localizationService;
            _datePickerTitle = datePickerTitle;
            _initialDateTime = initialDateTime;
        }

        public override async Task HandleAsync(
            ITelegramBotClient botClient, 
            Update update, 
            string[] args, 
            CancellationToken cancellationToken
            )
        {
            var calendarMarkup = MarkupHelper.Calendar(_initialDateTime.HasValue? _initialDateTime.Value : DateTime.Today, _localizationService.DateCulture);

            await botClient.SendTextMessageAsync(
                update.Message.Chat.Id,
                _datePickerTitle ?? "DatePicker: ",
                replyMarkup: calendarMarkup,
                cancellationToken: cancellationToken
            );
        }
    }
}
