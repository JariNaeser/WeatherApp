using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MeteoAppSkeleton.Models;
using MeteoAppSkeleton.ViewModels;
using Xamarin.Forms;

namespace MeteoAppSkeleton.Views
{
    public partial class MeteoListPage : ContentPage
    {
        public MeteoListPage()
        {
            InitializeComponent();
            BindingContext = new MeteoListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            string newLocationName = await DisplayPromptAsync("Add a new Location", "Insert Location name");

            Location newLocation = new Location(newLocationName);

            // Persist location

            // Add newLocation to database

            // Update _locations in MeteoListViewModel
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