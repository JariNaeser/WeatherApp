using System;
using Plugin.Geolocator;
using System.Diagnostics;
using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.Linq;

namespace MeteoAppSkeleton.Models
{
	public class LocalizatorModel
	{
        private static LocalizatorModel myself;
        private ObservableCollection<Location> _locations;
        private HttpModel httpModel;

        public static LocalizatorModel GetInstance
        {
            get { 
                if(myself == null)
                {
                    myself = new LocalizatorModel();
                }
                return myself;
            }
        }

		private LocalizatorModel(){
            httpModel = HttpModel.GetInstance;
        }

        public async Task StartListening(ObservableCollection<Location> locations)
        {
            _locations = locations;

            if (CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

            CrossGeolocator.Current.PositionChanged += PositionChanged;
            CrossGeolocator.Current.PositionError += PositionError;
        }

        private void PositionChanged(object sender, PositionEventArgs e)
        {
            //If updating the UI, ensure you invoke on main thread
            var position = e.Position;

            // Update current position
            // Get current Location from list
            Location myLocation = _locations.FirstOrDefault(l => l.Id == 1);

            // Get current location name
            myLocation.Name = httpModel.getLocationNameFromCoordinates(position.Latitude, position.Longitude);
        }

        private void PositionError(object sender, PositionErrorEventArgs e)
        {
            Debug.WriteLine(e.Error);
        }

        async Task StopListening()
        {
            if (!CrossGeolocator.Current.IsListening)
                return;

            //await CrossGeolocator.Current.StopListeningAsync;

            CrossGeolocator.Current.PositionChanged -= PositionChanged;
            CrossGeolocator.Current.PositionError -= PositionError;
        }
    }
}

