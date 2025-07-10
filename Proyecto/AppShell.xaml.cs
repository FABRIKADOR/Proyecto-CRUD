using Proyecto.Views;    // Para resolver LoginPage y UsuariosPage
using Microsoft.Maui.Controls;

namespace Proyecto
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(UsuariosPage), typeof(UsuariosPage));
            Routing.RegisterRoute(nameof(MainMenuPage), typeof(MainMenuPage));
            Routing.RegisterRoute(nameof(ClipboardPage), typeof(ClipboardPage));
            Routing.RegisterRoute(nameof(AccelerometerPage), typeof(AccelerometerPage));
        }
    }

}
