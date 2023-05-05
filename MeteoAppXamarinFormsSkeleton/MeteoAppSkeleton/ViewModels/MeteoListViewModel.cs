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
            _ = localizatorModel.StartListening(_locations);

            // Add location that will be replaced with the current one
            Locations.Add(new Location(1, "CurrentPosition"));

            // Read locations from database and insert them into _locations
            Locations.Add(new Location("London"));
            Locations.Add(new Location("Paris"));
            Locations.Add(new Location("Madrid"));

            // Test http
            HttpModel httpModel = HttpModel.GetInstance;
            WeatherCondition weatherInLugano = httpModel.getWeatherFromLocationAsync("Lugano");
        }
    }
}