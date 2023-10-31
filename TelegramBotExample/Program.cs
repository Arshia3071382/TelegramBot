// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Telegram.Bot;
using TelegramBotExample;
using System.Net.Http;
using Telegram.Bot.Args; //عرشیا مختاری -تمرین ششم - بات تلگرام
using Telegram.Bot.Types;




var apiToken = "token";
var botClient = new TelegramBotClient(apiToken);

botClient.OnMessage += async (sender, e) =>
{
    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
    {
        var cityName = e.Message.Text;
        var ApiResponse = await GetCityInformation(cityName);
        if (ApiResponse != null)
        {

            await botClient.SendTextMessageAsync(e.Message.Chat.Id, $"Information for {cityName}:\n{ApiResponse}");
        }

    }
};

botClient.StartReceiving();

Console.WriteLine("Telegram bot started. Press any key to exit.");
Console.ReadLine();

botClient.StopReceiving();


static async Task<string> GetCityInformation(string cityName)
{
    string apiUrl = $"http://api.aladhan.com/v1/timingsByCity?city={cityName}&country=Islamic+Republic+Of+Iran&method=7";

    using (var httpClient = new HttpClient())
    {
        var response = await httpClient.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var ApiResponse = await response.Content.ReadAsStringAsync();

            ApiResponse apiWrapper = JsonConvert.DeserializeObject<ApiResponse>(ApiResponse);

            return $"Azan sobh be ofogh {cityName}: {apiWrapper?.data.timing.Fajr}"
                + $"\n Azan Asr be ofogh {cityName}: {apiWrapper?.data.timing.Asr}"
                + $"\n Azan Maghrib be ofogh {cityName}: {apiWrapper?.data.timing.Maghrib}"
                + $"\n Nime shab sharei be ofogh {cityName}: {apiWrapper?.data.timing.Midnight}"
                + $"\n Tolo Aftab {cityName}: {apiWrapper?.data.timing.Sunrise}"
                + $"\n ghorob Aftab {cityName}: {apiWrapper?.data.timing.Sunset}";
        }
        else
        {

            return "nam shahr khod ra vared konid(mesal = Qom)";
        }
    }
}



