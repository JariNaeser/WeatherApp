using System;
using System.Globalization;
using MeteoAppSkeleton.Models;
using Xamarin.Forms;

namespace MeteoAppSkeleton.Helper
{
	public class IconLinkConverter : IValueConverter
	{
		public IconLinkConverter()
		{
		}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Value is the locationName for the query, has to be string
            //if (value is not string) return null;

            HttpModel httpModel = HttpModel.GetInstance;
            WeatherCondition weatherInLocation = httpModel.getWeatherFromLocationAsync((string)value);

            return "http://openweathermap.org/img/wn/" + weatherInLocation.Icon + "@4x.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

