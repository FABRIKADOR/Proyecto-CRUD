using Microsoft.Maui;

namespace Proyecto.Views;

public partial class ClipboardPage : ContentPage
{
    public ClipboardPage()
    {
        InitializeComponent();
    }

    async void OnCopyClicked(object sender, EventArgs e)
        => await Clipboard.Default.SetTextAsync(TextInput.Text);

    async void OnPasteClicked(object sender, EventArgs e)
    {
        if (Clipboard.Default.HasText)
        {
            string text = await Clipboard.Default.GetTextAsync();
            OutputLabel.Text = $"Pegar: {text}";
        }
    }
}
