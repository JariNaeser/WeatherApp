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

            // Read locations from database and insert them into _locations

            Locations.Add(new Location(1, "CurrentPosition"));
            Locations.Add(new Location(2, "London"));
            Locations.Add(new Location(3, "Paris"));
            Locations.Add(new Location(4, "Madrid"));

            // Test http
            HttpModel httpModel = HttpModel.GetInstance;
            WeatherCondition weatherInLugano = httpModel.getWeatherFromLocationAsync("Lugano");
        }
    }
}