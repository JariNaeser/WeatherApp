using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MeteoAppSkeleton.Models;
using MeteoAppSkeleton.ViewModels;
using Xamarin.Forms;
using static SQLite.SQLite3;



namespace MeteoAppSkeleton.Views
{
    public partial class MeteoListPage : ContentPage
    {

        MeteoListViewModel meteo;

        public MeteoListPage()
        {
            InitializeComponent();
            meteo = new MeteoListViewModel();
            BindingContext = meteo;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var locationToRemove = (Location)((Button)sender).CommandParameter;
            meteo.Locations.Remove(locationToRemove);
            await App.Database.DeleteItemAsync(locationToRemove);
        }


        async void OnItemAdded(object sender, EventArgs e)
        {
            string newLocationName = await DisplayPromptAsync("Add a new Location", "Insert Location name");

            Location newLocation = new Location {  Name = newLocationName };

            // Add newLocation to database

            await App.Database.SaveItemAsync(newLocation);

            // Add newLocation to _locations in MeteoListViewModel

            meteo.Locations.Add(newLocation);
        }

        private async Task<string> ShowAddLocationPromptAndGetResponseAsync()
        {
            return await DisplayPromptAsync("Add a new Location", "Insert Location name");
        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage()
                {
                    BindingContext = e.SelectedItem as Models.Location
                });
            }
        }
    }
}