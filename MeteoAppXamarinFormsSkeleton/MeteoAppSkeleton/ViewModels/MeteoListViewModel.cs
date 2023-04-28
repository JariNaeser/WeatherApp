using System.Collections.ObjectModel;
using System.Diagnostics;
using MeteoAppSkeleton.Models;

namespace MeteoAppSkeleton.ViewModels
{
    public class MeteoListViewModel : BaseViewModel
    {
        ObservableCollection<Location> _locations;

        public ObservableCollection<Location> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }

        public MeteoListViewModel()
        {
            Locations = new ObservableCollection<Location>();

            // Read locations from database and insert them into _locations

            for (var i = 0; i < 10; i++)
            {
                var e = new Location
                {
                    ID = i,
                    Name = "Location " + i
                };

                Locations.Add(e);
            }

            // Test http

            HttpModel httpModel = HttpModel.GetInstance;

            Debug.WriteLine(httpModel.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=London,uk&appid=2029667e5e8d87ff1f9a29a34238d79f"));

            // _ = httpModel.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=London,uk&appid=2029667e5e8d87ff1f9a29a34238d79f");

            //WeatherCondition weather = httpModel.GetWeatherConditionAsync("https://api.openweathermap.org/data/2.5/weather?q=London,uk&appid=2029667e5e8d87ff1f9a29a34238d79f");

            //Debug.WriteLine("Weather: "  + weather);

        }
    }
}