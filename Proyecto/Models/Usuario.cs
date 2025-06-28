using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace Proyecto.Models
{
    [Table("usuariosmaui")]
    public class Usuario : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
