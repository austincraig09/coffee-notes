using System;
using Microsoft.Maui.Controls;

namespace CoffeeNotesApp2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            await CoffeeNoteDatabase.InitializeDatabaseAsync();
        }

        private async void OnImportPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var photoResult = await MediaPicker.PickPhotoAsync();
                if (photoResult != null)
                {
                    var stream = await photoResult.OpenReadAsync();
                    await NavigateToImageProcessingPage(stream);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Photo import failed: {ex.Message}", "OK");
            }
        }

        private async void OnViewNotesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoffeeNotesPage());
        }

        private async Task NavigateToImageProcessingPage(Stream imageStream)
        {
            await Navigation.PushAsync(new ImageProcessingPage(imageStream));
        }
    }
}
