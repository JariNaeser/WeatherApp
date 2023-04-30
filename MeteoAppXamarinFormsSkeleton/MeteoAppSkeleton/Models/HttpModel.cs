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

		private HttpModel() { }

		private async Task<string> GetAsync(string url)
		{
			// Make request
			var httpClient = new HttpClient();
			return await httpClient.GetStringAsync(url);
        }

		private WeatherCondition GetWeatherConditionAsync(string url)
		{
            // Get response
            var result = Task.Run(async () => await GetAsync(url)).Result;

			return new WeatherCondition(JObject.Parse(result));
        }

		public WeatherCondition getWeatherFromLocationAsync(string location)
		{
			string query = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + API_KEY;
			return this.GetWeatherConditionAsync(query);
		}
	}
}

