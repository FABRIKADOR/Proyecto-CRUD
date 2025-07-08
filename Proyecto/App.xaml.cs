namespace Proyecto
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell(); // ← Aquí está el cambio importante
        }
    }
}
