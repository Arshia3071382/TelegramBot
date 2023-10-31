using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TelegramBotExample
{
    public class ApiResponse
    {
        [JsonProperty("data")]
        public Data data { get; set; }


        [JsonProperty("status")]
        public string? status { get; set; }


    }
}
