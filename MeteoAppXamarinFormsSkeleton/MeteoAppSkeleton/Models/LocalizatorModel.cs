using System;
using Plugin.Geolocator;
using System.Diagnostics;
using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;

namespace MeteoAppSkeleton.Models
{
	public class LocalizatorModel
	{
        private static LocalizatorModel myself;

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

		private LocalizatorModel(){}

        public async Task StartListening()
        {
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









            Debug.WriteLine("Current position: Lat: " + position.Latitude + " Long: " + position.Longitude);
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

