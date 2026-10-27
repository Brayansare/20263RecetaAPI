using System.ComponentModel.DataAnnotations;

namespace _20263RecetaAPI.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        [Required]
        [StringLength(800)]
        public string Texto { get; set; } = string.Empty;
        public DateTime Creacion { get; set; } = DateTime.UtcNow;

        //llaves foraneas
        public int IdReceta { get; set; }
        public string UsuarioId { get; set; } = default!;

        //propiedades de navegacion 
        public Receta? Receta { get; set; }
        public ApplicationUser? Usuario { get; set; }
    }
}
