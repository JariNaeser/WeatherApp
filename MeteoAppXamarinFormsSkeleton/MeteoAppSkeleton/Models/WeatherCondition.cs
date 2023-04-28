using System;
namespace MeteoAppSkeleton.Models
{
	public class WeatherCondition
	{
        private int Id { get; set; }
        private String WeatherType { get; set; }
        private String Description { get; set; }
        private String Icon { get; set; }
        private double Temperature { get; set; }
        private int Pressure { get; set; }
        private int Humidity { get; set; }
        private double MinTemperature { get; set; }
        private double MaxTemperature { get; set; }
        private String CityName { get; set; }
        private String CountryCode { get; set; }
        private long Sunrise { get; set; }
        private long Sunset { get; set; }
        private double WindSpeed { get; set; }

        public WeatherCondition(int id, String weatherType, String description,
                             String icon, double temperature, int pressure,
                             int humidity, double minTemperature, double maxTemperature,
                             String cityName, String countryCode, long sunrise,
                             long sunset, double windSpeed)
		{
            Id = id;
            WeatherType = weatherType;
            Description = description;
            Icon = icon;
            Temperature = temperature;
            Pressure = pressure;
            Humidity = humidity;
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
            CityName = cityName;
            CountryCode = countryCode;
            Sunrise = sunrise;
            Sunset = sunset;
            WindSpeed = windSpeed;
		}
	}
}

