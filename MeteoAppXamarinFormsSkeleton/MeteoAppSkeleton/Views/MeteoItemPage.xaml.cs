using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MeteoAppSkeleton.Models;
using Plugin.LocalNotification;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;

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

            Console.WriteLine(SelectedLocation.Name);
            
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

            if (SelectedLocation.Name.Equals("CurrentPosition") && Round(weather.Temperature) >= 0)
            {
                _ = ShowNotification(SelectedLocation.Name,Round(weather.Temperature));
            }
        }
        
        private int Round(double number)
        {
            return (int)(number + 0.5);
        }

        private async Task ShowNotification(string nameLocation,int temperature)
        {
            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "WeatherApp",
                Description = nameLocation + "si percepiscono :" + temperature,
                ReturningData = "Dummy data", // Returning data when tapped on notification.
                Schedule =
                {
                    // Used for Scheduling local notification, if not specified notification will show immediately.
                    NotifyTime = DateTime.Now.AddSeconds(3) 
                }
            };

            await LocalNotificationCenter.Current.Show(notification);
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