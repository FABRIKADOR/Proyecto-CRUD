using Proyecto.Models;
using Proyecto.Services;

namespace Proyecto.Views;

public partial class UsuariosPage : ContentPage
{
    private readonly SupabaseService _supabaseService;

    public UsuariosPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
        CargarUsuarios();
    }

    private async void CargarUsuarios()
    {
        var usuarios = await _supabaseService.ObtenerUsuarios();
        usuariosCollection.ItemsSource = usuarios;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        string nombre = await DisplayPromptAsync("Nuevo Usuario", "Ingresa el nombre completo:");
        if (string.IsNullOrWhiteSpace(nombre))
            return;

        string email = await DisplayPromptAsync("Nuevo Usuario", "Ingresa el correo electrónico:");
        if (string.IsNullOrWhiteSpace(email))
            return;

        var usuario = new Usuario
        {
            FullName = nombre,
            Email = email
        };

        bool exito = await _supabaseService.AgregarUsuario(usuario);

        if (exito)
        {
            await DisplayAlert("Éxito", "Usuario agregado correctamente", "OK");
            CargarUsuarios();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo agregar el usuario", "OK");
        }
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var usuario = button?.BindingContext as Usuario;
        if (usuario == null) return;

        string nuevoNombre = await DisplayPromptAsync("Editar Usuario", "Nuevo nombre:", initialValue: usuario.FullName);
        if (string.IsNullOrWhiteSpace(nuevoNombre)) return;

        string nuevoEmail = await DisplayPromptAsync("Editar Usuario", "Nuevo correo electrónico:", initialValue: usuario.Email);
        if (string.IsNullOrWhiteSpace(nuevoEmail)) return;

        usuario.FullName = nuevoNombre;
        usuario.Email = nuevoEmail;

        bool exito = await _supabaseService.ActualizarUsuario(usuario);

        if (exito)
        {
            await DisplayAlert("Éxito", "Usuario actualizado correctamente", "OK");
            CargarUsuarios();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo actualizar el usuario", "OK");
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var usuario = button?.BindingContext as Usuario;
        if (usuario == null) return;

        bool confirm = await DisplayAlert("Eliminar", $"¿Deseas eliminar a {usuario.FullName}?", "Sí", "No");
        if (!confirm) return;

        bool exito = await _supabaseService.EliminarUsuario(usuario);

        if (exito)
        {
            await DisplayAlert("Éxito", "Usuario eliminado", "OK");
            CargarUsuarios();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo eliminar el usuario", "OK");
        }
    }
}
