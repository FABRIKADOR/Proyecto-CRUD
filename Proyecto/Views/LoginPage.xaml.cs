using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Authentication;
using Proyecto.Models;
using Proyecto.Services;

namespace Proyecto.Views
{
    public partial class LoginPage : ContentPage
    {
        // 1) Tu servicio de Supabase para el CRUD
        readonly SupabaseService _supabaseService = new();

        // 2) Tus credenciales de Facebook
        const string AppId = "1378516639901435";
        const string AppSecret = "95d64c918c1ad519c6731f56440377a2";
        const string RedirectUri = "https://vtraxawdyzwgujhlcdyf.supabase.co/auth/v1/callback";

        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnFacebookLoginClicked(object sender, EventArgs e)
        {
            btnFacebookLogin.IsEnabled = false;
            lblResult.Text = "Abriendo Facebook…";

            try
            {
                // ——— Paso A: login OAuth y obtener access token ———
                var token = await LoginWithFacebook();
                if (token is null)
                {
                    lblResult.Text = "Error al iniciar sesión.";
                    return;
                }

                // ——— Paso B: pide perfil a Graph API ———
                lblResult.Text = "Obteniendo perfil…";
                using var client = new HttpClient();
                var profileUrl =
                    "https://graph.facebook.com/v12.0/me" +
                    "?fields=name,email" +
                    $"&access_token={token}";
                var fbUser = await client.GetFromJsonAsync<GraphUser>(profileUrl);

                if (fbUser is null || string.IsNullOrWhiteSpace(fbUser.Email))
                {
                    lblResult.Text = "No se obtuvo email de Facebook.";
                    return;
                }

                // ——— Paso C: guarda en tu tabla de Supabase ———
                lblResult.Text = "Guardando usuario…";
                var usuario = new Usuario
                {
                    FullName = fbUser.Name,
                    Email = fbUser.Email
                };

                bool ok = await _supabaseService.AgregarUsuario(usuario);
                if (ok)
                {
                    lblResult.Text = "¡Usuario registrado! Redirigiendo...";
                    await Shell.Current.GoToAsync("//UsuariosPage");
                }
                else
                {
                    lblResult.Text = "Error al guardar en Supabase.";
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = $"Error inesperado: {ex.Message}";
            }
            finally
            {
                btnFacebookLogin.IsEnabled = true;
            }
        }

        async Task<string?> LoginWithFacebook()
        {
            // Construye la URL de OAuth de Facebook
            var oauthUrl =
                "https://www.facebook.com/v12.0/dialog/oauth" +
                $"?client_id={AppId}" +
                $"&redirect_uri={Uri.EscapeDataString(RedirectUri)}" +
                "&response_type=code" +
                "&scope=email,public_profile";

            // Abre el navegador y espera el deep-link de vuelta
            var authResult = await WebAuthenticator.Default.AuthenticateAsync(
                new Uri(oauthUrl),
                new Uri(RedirectUri)
            );

            // Extrae el código de autorización
            if (!authResult.Properties.TryGetValue("code", out var code))
                return null;

            // Intercambia el código por access token
            using var client = new HttpClient();
            var tokenUrl =
                "https://graph.facebook.com/v12.0/oauth/access_token" +
                $"?client_id={AppId}" +
                $"&redirect_uri={Uri.EscapeDataString(RedirectUri)}" +
                $"&client_secret={AppSecret}" +
                $"&code={code}";

            var resp = await client.GetFromJsonAsync<FacebookTokenResponse>(tokenUrl);
            return resp?.access_token;
        }
    }
}
