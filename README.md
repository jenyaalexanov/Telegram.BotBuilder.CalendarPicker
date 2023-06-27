# Telegram.BotBuilder.CalendarPicker

![Nuget](https://img.shields.io/nuget/v/Telegram.BotBuilder.CalendarPicker?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/dt/Telegram.BotBuilder.CalendarPicker?style=for-the-badge)
[![GitHub license](https://img.shields.io/github/license/jenyaalexanov/Telegram.BotBuilder.CalendarPicker?style=for-the-badge)](https://github.com/jenyaalexanov/Telegram.BotBuilder.CalendarPicker/blob/master/LICENSE)

<img src="./icons/CalendarPicker.Large.png" alt="Telegram CalendarPicker Logo" width=200 height=200 />

- Telegram.BotBuilder.CalendarPicker is a library that allows you to pick a date from your telegram bot.
- Based on [`CalendarPicker`](https://github.com/karb0f0s/CalendarPicker) but modified for [`Telegram.BotBuilder`](https://github.com/jenyaalexanov/Telegram.BotBuilder) and fixed some bugs and errors.
- Works with new version of TelegramAPI [`Telegram.Bot`](https://github.com/TelegramBots/Telegram.Bot) [`18.0.0-alpha.3`](https://www.nuget.org/packages/Telegram.Bot/18.0.0-alpha.3)
- Based on dotnet standard 2.1

  >And of course, I made this library for my own use, I believe that it can be useful when you are going to develop complex Telegram bots.
  >I am always open to your changes, commits and wishes :)

How do I get started?
--------------
Add Telegram.BotBuilder.CalendarPicker to your project:

**Package Manager**

	PM> NuGet\Install-Package DistributedLock.MongoDatabase -Version 3.1.1
  
**.NET CLI**

	>dotnet add package Telegram.BotBuilder.CalendarPicker
  
**or something else**

--------------

Next you should create your **handler** inherited from **ICalendarHandler** to **handle picked date** form telegram bot. For example:

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
    
After that you can add a **calendar picker** with a **handler**
    
    // add calendar with handler
    builder.Services.AddCalendarPicker<CalendarTestHandler>("nameOfYourBot", "tokenOfBot");
    
**AddCalendarPicker<>** supports various features, including: 
- **locale**, if you need a locale;
- **datePickerTitle**, for a title of datepicker message; 
- **initialDate**, to select a specific start date;

Next you can bind using **LongPolling** with adding command string:

    // bind calendar as LongPolling
    await app.UseCalendarPickerLongPolling("calendar");
    
Or as Webhook with adding command string and uri of your webhook:

    // or as Webhook
    await app.UseCalendarPickerWebhook("calendar", new Uri("https://example.com/calendarbot/webhook"));

You can find **all these examples** and **work with them** in the [webapi project](https://github.com/jenyaalexanov/Telegram.BotBuilder.CalendarPicker/tree/master/Telegram.BotBuilder.CalendarPicker.WebApi)
