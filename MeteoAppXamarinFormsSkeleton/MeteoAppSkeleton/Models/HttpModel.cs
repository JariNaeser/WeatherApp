using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeteoAppSkeleton.Models
{
	public class HttpModel
	{
		private static HttpModel myself;

		public HttpModel getInstance()
		{
			if(myself == null)
			{
				myself = new HttpModel();
			}
			return myself;
		}

		private HttpModel() { }

		public async Task<WeatherCondition> GetAsync(String url)
		{
			// Make request
			var httpClient = new HttpClient();
			var jsonResponse = await httpClient.GetStringAsync(url);

			// Parse response
			return JsonConvert.DeserializeObject<WeatherCondition>(jsonResponse);
        }
	}
}

