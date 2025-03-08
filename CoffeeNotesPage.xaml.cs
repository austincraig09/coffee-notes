namespace CoffeeNotesApp2;

public partial class CoffeeNotesPage : ContentPage
{
    public CoffeeNotesPage()
    {
        InitializeComponent();
        LoadCoffeeNotes();
    }

    private async void LoadCoffeeNotes()
    {
        var notes = await CoffeeNoteDatabase.GetNotesAsync();
        CoffeeNotesCollection.ItemsSource = notes;
    }
}
