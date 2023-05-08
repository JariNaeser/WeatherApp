using System;
using System.Diagnostics;
using MeteoAppSkeleton.Models;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;

namespace MeteoAppSkeleton.Views
{
    public partial class MeteoItemPage : ContentPage
    {

        private Location SelectedLocation { get; set; }
        private HttpModel httpModel;
        WeatherCondition weather;

        public MeteoItemPage()
        {
            InitializeComponent();
            httpModel = HttpModel.GetInstance;
        }

        // Method that gets invoked when page content has loaded
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Get selected Location
            SelectedLocation = BindingContext as Location;
            // Get weatherCondition in selected Location
            
            weather = httpModel.getWeatherFromLocationAsync(SelectedLocation.Name);
          
            // Set text on UI
            lDescription.Text = weather.Description;
            lTemperature.Text = Round(weather.Temperature) + " °C";
            lMinTemperature.Text = "Min: " + Round(weather.MinTemperature) + "°";
            lMaxTemperature.Text = "Max: " + Round(weather.MaxTemperature) + "°";
            lPressure.Text = weather.Pressure + " [Pa]";
            lHumidity.Text = weather.Humidity + " %";
            lSunrise.Text = FormatDate(weather.Sunrise);
            lSunset.Text = FormatDate(weather.Sunset);
            // Images
            iMeteo.Source = "http://openweathermap.org/img/wn/" + weather.Icon + "@4x.png";
            iFlag.Source = "https://flagsapi.com/" + weather.CountryCode.ToUpper() + "/flat/64.png";
            
        }

        private int Round(double number)
        {
            return (int)(number + 0.5);
        }

        private DateTime CreateDateFromMillis(long unixMillis)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixMillis).ToLocalTime();
            return dateTime;
        }

        private string FormatDate(long unixMillis)
        {
            DateTime dateTime = CreateDateFromMillis(unixMillis);
            return String.Format("{0:D2} : {1:D2} : {2:D2}", dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

    }
}