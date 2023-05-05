using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MeteoAppSkeleton.Models
{
	public class HttpModel
	{
		private static HttpModel myself;
		private HttpClient httpClient;
		private const string API_KEY = "2029667e5e8d87ff1f9a29a34238d79f";

        public static HttpModel GetInstance
		{
			get
			{ 
				if(myself == null)
				{
					myself = new HttpModel();
				}
				return myself;
            }
        }

		private HttpModel() {
			httpClient = new HttpClient();
        }

		private async Task<string> GetRequestAsync(string url)
		{
			return await httpClient.GetStringAsync(url);
        }

		public WeatherCondition getWeatherFromLocationAsync(string location)
		{
			string query = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=" + API_KEY;

            var result = Task.Run(async () => await GetRequestAsync(query)).Result;
            return new WeatherCondition(JObject.Parse(result));
        }

		public string getLocationNameFromCoordinates(double lat, double lon)
		{
			string query = "https://api.openweathermap.org/geo/1.0/reverse?lat=" + lat + "&lon=" + lon + "&limit=1&appid=" + API_KEY;

			var result = Task.Run(async () => await GetRequestAsync(query)).Result;
            var jObject = JObject.Parse(result.Replace("[", "").Replace("]", ""));
            return jObject["name"].ToObject<string>();
        }

    }
}

