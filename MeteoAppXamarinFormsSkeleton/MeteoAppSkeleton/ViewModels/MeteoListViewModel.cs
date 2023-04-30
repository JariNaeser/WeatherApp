using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
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

            // Start current localization listener
            LocalizatorModel localizatorModel = LocalizatorModel.GetInstance;
            _ = localizatorModel.StartListening();

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
            WeatherCondition weatherInLugano = httpModel.getWeatherFromLocationAsync("Lugano");
        }
    }
}