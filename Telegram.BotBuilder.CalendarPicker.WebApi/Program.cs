using Telegram.BotBuilder.CalendarPicker.WebApi;
using Telegram.BotBuilder.CalendarPicker.Extensions;

var builder = WebApplication.CreateBuilder(args);

// add calendar with handler
builder.Services.AddCalendarPicker<CalendarTestHandler>("nameOfYourBot", "tokenOfBot");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// bind calendar as LongPolling
//await app.UseCalendarPickerLongPolling("calendar");

// or as Webhook
await app.UseCalendarPickerWebhook("calendar", new Uri("https://example.com/calendarbot/webhook"));

app.Run();
