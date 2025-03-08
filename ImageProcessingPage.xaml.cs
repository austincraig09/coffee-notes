namespace CoffeeNotesApp2;

public partial class ImageProcessingPage : ContentPage
{
    private byte[] _imageData;

    public ImageProcessingPage(Stream imageStream)
    {
        InitializeComponent();

        using (var memoryStream = new MemoryStream())
        {
            imageStream.CopyTo(memoryStream);
            _imageData = memoryStream.ToArray();
        }

        ImportedImage.Source = ImageSource.FromStream(() => new MemoryStream(_imageData));
    }

    private async void OnSaveCoffeeNoteClicked(object sender, EventArgs e)
    {
        if (
            string.IsNullOrWhiteSpace(BrandEntry.Text) || string.IsNullOrWhiteSpace(RoastEntry.Text)
        )
        {
            await DisplayAlert("Validation Error", "Please enter brand and roast", "OK");
            return;
        }

        var savedImagePath = await SaveImageToLocal();
        var coffeeNote = new CoffeeNote
        {
            Brand = BrandEntry.Text,
            Roast = RoastEntry.Text,
            ImportDate = DateTime.Now,
            ImagePath = savedImagePath,
        };
        await CoffeeNoteDatabase.SaveNoteAsync(coffeeNote);
        await Navigation.PopToRootAsync();
    }

    private async Task<string> SaveImageToLocal()
    {
        var fileName = $"coffee_{Guid.NewGuid()}.jpg";
        var savedPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        using (var fileStream = File.OpenWrite(savedPath))
        using (var imageStream = new MemoryStream(_imageData))
        {
            await imageStream.CopyToAsync(fileStream);
        }

        return savedPath;
    }
}
