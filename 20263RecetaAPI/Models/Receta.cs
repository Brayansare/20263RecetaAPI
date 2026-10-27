using System.ComponentModel.DataAnnotations;

namespace _20263RecetaAPI.Models
{
    public class Receta
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        public int Porciones { get; set; }
        public int Tiempo { get; set; }

        public string? Instrucciones { get; set; }

        public DateTime Creacion { get; set; } = DateTime.UtcNow;

        [StringLength(250)]
        public string? Descripcion { get; set; }

        // llaves foraneas
        public string UsuarioId { get; set; } = string.Empty;
        public int IdCategoria { get; set; }

        // propiedades de navegacion
        public ApplicationUser? Usuario { get; set; }
        public Category? Categoria { get; set; }
        public ICollection<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();

        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}