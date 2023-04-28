using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeteoAppSkeleton.Models
{
	public class HttpModel
	{
		private static HttpModel myself;

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

		public async Task<string> GetAsync(string url)
		{
			// Make request
			var httpClient = new HttpClient();
			return await httpClient.GetStringAsync(url);
        }

		public WeatherCondition GetWeatherConditionAsync(string url)
		{
			string json = GetAsync(url).Result;
            // Parse response
            return JsonConvert.DeserializeObject<WeatherCondition>(json);
        }
	}
}

