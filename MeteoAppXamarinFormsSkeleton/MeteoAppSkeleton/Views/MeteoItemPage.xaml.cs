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
            SelectedLocation.Weather = httpModel.getWeatherFromLocationAsync(SelectedLocation.Name);
          
            // Set text on UI
            lDescription.Text = SelectedLocation.Weather.Description;
            lTemperature.Text = Round(SelectedLocation.Weather.Temperature) + " °C";
            lMinTemperature.Text = "Min: " + Round(SelectedLocation.Weather.MinTemperature) + "°";
            lMaxTemperature.Text = "Max: " + Round(SelectedLocation.Weather.MaxTemperature) + "°";
            lPressure.Text = SelectedLocation.Weather.Pressure + " [Pa]";
            lHumidity.Text = SelectedLocation.Weather.Humidity + " %";
            lSunrise.Text = FormatDate(SelectedLocation.Weather.Sunrise);
            lSunset.Text = FormatDate(SelectedLocation.Weather.Sunset);
            // Images
            iMeteo.Source = "http://openweathermap.org/img/wn/" + SelectedLocation.Weather.Icon + "@4x.png";
            iFlag.Source = "https://flagsapi.com/" + SelectedLocation.Weather.CountryCode.ToUpper() + "/flat/64.png";
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