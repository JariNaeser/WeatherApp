using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace MeteoAppSkeleton.Models
{
	public class WeatherCondition
	{
        public int Id { get; set; }
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

        public WeatherCondition(JObject json)
        {
            if (CheckIfFieldsExist(json))
            {
                Id = json["weather"][0]["id"].ToObject<int>();
                WeatherType = json["weather"][0]["main"].ToObject<string>();
                Description = json["weather"][0]["description"].ToObject<string>();
                Icon = json["weather"][0]["icon"].ToObject<string>();
                Temperature = json["main"]["temp"].ToObject<double>();
                Pressure = json["main"]["pressure"].ToObject<int>();
                Humidity = json["main"]["humidity"].ToObject<int>();
                MinTemperature = json["main"]["temp_min"].ToObject<double>();
                MaxTemperature = json["main"]["temp_max"].ToObject<double>();
                CityName = json["name"].ToObject<string>();
                CountryCode = json["sys"]["country"].ToObject<string>();
                Sunrise = json["sys"]["sunrise"].ToObject<long>();
                Sunset = json["sys"]["sunset"].ToObject<long>();
                WindSpeed = json["wind"]["speed"].ToObject<double>();
            }
        }

        public bool CheckIfFieldsExist(JObject json)
        {
            return json["weather"][0]["id"] != null
                && json["weather"][0]["main"] != null
                && json["weather"][0]["description"] != null
                && json["weather"][0]["icon"] != null
                && json["main"]["temp"] != null
                && json["main"]["pressure"] != null
                && json["main"]["humidity"] != null
                && json["main"]["temp_min"] != null
                && json["main"]["temp_max"] != null
                && json["name"] != null
                && json["sys"]["country"] != null
                && json["sys"]["sunrise"] != null
                && json["sys"]["sunset"] != null
                && json["wind"]["speed"] != null;
        }
	}
}

