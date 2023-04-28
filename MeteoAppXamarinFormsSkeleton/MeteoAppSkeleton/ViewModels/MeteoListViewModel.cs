using System.Collections.ObjectModel;
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
        }
    }
}