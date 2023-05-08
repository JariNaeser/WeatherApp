using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MeteoAppSkeleton.Models;
using Xamarin.Forms;
using System;
using System.Collections.Generic;

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



        public  MeteoListViewModel()
        {
            Locations = new ObservableCollection<Location>();

            // Start current localization listener
            LocalizatorModel localizatorModel = LocalizatorModel.GetInstance;
            _ = localizatorModel.StartListening(_locations);


            // Add location that will be replaced with the current one
            Locations.Add(new Location { Id = 1, Name = "CurrentPosition" });


            // Read locations from database and insert them into _locations
            Task.Run(async () =>
            {
                List<Location> locations = await App.Database.GetItemsAsync();
                foreach (var location in locations)
                {
                    Locations.Add(location);
                }
            });

            

            // Test http
            HttpModel httpModel = HttpModel.GetInstance;
            WeatherCondition weatherInLugano = httpModel.getWeatherFromLocationAsync("Lugano");


        }
    }
}