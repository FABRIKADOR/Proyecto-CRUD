namespace Proyecto.Models
{
    // Para deserializar el JSON de Facebook "/me?fields=name,email"
    public class GraphUser
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
