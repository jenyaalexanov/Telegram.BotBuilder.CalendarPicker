using System;
using System.Collections.Generic;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.BotBuilder.CalendarPicker.Helpers
{
    public static class MarkupHelper
    {
        public static InlineKeyboardMarkup Calendar(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>();

            keyboardRows.Add(RowHelper.Date(date, dtfi));
            keyboardRows.Add(RowHelper.DayOfWeek(dtfi));
            keyboardRows.AddRange(RowHelper.Month(date, dtfi));
            keyboardRows.Add(RowHelper.Controls(date));

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickMonthYear(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        date.ToString("MMMM", dtfi),
                        $"{Constants.PickMonth}{date.ToString(Constants.DateFormat)}"
                    ),
                    InlineKeyboardButton.WithCallbackData(
                        date.ToString("yyyy", dtfi),
                        $"{Constants.PickYear}{date.ToString(Constants.DateFormat)}"
                    )
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        "<<",
                        $"{Constants.ChangeTo}{date.ToString(Constants.DateFormat)}"
                    ),
                    " "
                }
            };

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickMonth(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[5][];

            for (int month = 0, row = 0; month < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, month++)
                {
                    var day = new DateTime(date.Year, month + 1, 1);

                    keyboardRow[j] = InlineKeyboardButton.WithCallbackData(
                        dtfi.MonthNames[month],
                        $"{Constants.YearMonthPicker}{day.ToString(Constants.DateFormat)}"
                    );
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = RowHelper.BackToMonthPicker(date);

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickYear(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[5][];

            var startYear = date.AddYears(-7);

            for (int i = 0, row = 0; i < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, i++)
                {
                    var day = startYear.AddYears(i);

                    keyboardRow[j] = InlineKeyboardButton.WithCallbackData(
                        day.ToString("yyyy", dtfi),
                        $"{Constants.YearMonthPicker}{day.ToString(Constants.DateFormat)}"
                    );
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = RowHelper.BackToYearPicker(date);

            return new InlineKeyboardMarkup(keyboardRows);
        }
    }
}
