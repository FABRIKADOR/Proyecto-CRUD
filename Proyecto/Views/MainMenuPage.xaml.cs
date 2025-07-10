namespace Proyecto.Views;

public partial class MainMenuPage : ContentPage
{
    public MainMenuPage()
    {
        InitializeComponent();
    }

    private async void OnAccelerometerClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AccelerometerPage));
    }

    private async void OnClipboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClipboardPage));
    }

    private async void OnFacebookClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}
