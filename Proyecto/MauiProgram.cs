using Microsoft.Extensions.Logging;
using Proyecto.Services;
using Proyecto.Views;
using Supabase;

namespace Proyecto
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(f =>
                {
                    f.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    f.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // 1) Cliente Supabase
            builder.Services.AddSingleton(_ => new Supabase.Client(
                "https://vtraxawdyzwgujhlcdyf.supabase.co",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ0cmF4YXdkeXp3Z3VqaGxjZHlmIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA5MTA1NjMsImV4cCI6MjA2NjQ4NjU2M30.PTKE_api4j89z4Jtpr6HxI7yT_nLHo092AIKmaCoWWA",
                new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = true
                }
            ));

            // 2) Servicios
            builder.Services.AddSingleton<SupabaseService>();

            // 3) Páginas
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<UsuariosPage>();


            return builder.Build();
        }
    }
}
