using Proyecto.Models;
using Supabase;

namespace Proyecto.Services
{
    public class SupabaseService
    {
        private readonly Supabase.Client _client;
        private bool _initialized = false;

        public SupabaseService()
        {
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            _client = new Supabase.Client(
                "https://vtraxawdyzwgujhlcdyf.supabase.co",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ0cmF4YXdkeXp3Z3VqaGxjZHlmIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA5MTA1NjMsImV4cCI6MjA2NjQ4NjU2M30.PTKE_api4j89z4Jtpr6HxI7yT_nLHo092AIKmaCoWWA",
                options);
        }

        public async Task InitializeAsync()
        {
            if (!_initialized)
            {
                await _client.InitializeAsync();
                _initialized = true;
            }
        }

        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            await InitializeAsync();
            var response = await _client.From<Usuario>().Get();
            return response.Models;
        }

        public async Task<bool> AgregarUsuario(Usuario usuario)
        {
            await InitializeAsync();
            usuario.Id = Guid.NewGuid();
            usuario.CreatedAt = DateTime.UtcNow;

            var response = await _client.From<Usuario>().Insert(usuario);
            return response.Models.Any();
        }

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            await InitializeAsync();
            var response = await _client.From<Usuario>().Update(usuario);
            return response.ResponseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarUsuario(Usuario usuario)
        {
            await InitializeAsync();
            var response = await _client.From<Usuario>().Delete(usuario);
            return response.ResponseMessage.IsSuccessStatusCode;
        }
    }
}
