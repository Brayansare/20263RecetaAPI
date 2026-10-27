namespace _20263RecetaAPI.Models
{
    namespace _20263RecetaAPI.Models
    {
        public class Ingrediente
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = default!;
            public string Descripcion { get; set; } = default!;
            public string Medida { get; set; } = default!;
            public DateTime Creacion { get; set; } = DateTime.UtcNow;
            //un ingrediente pertenece a muchas recetas 
            //atraves de la tabla intermedia
            public required ICollection<RecetaIngrediente> RecetaIngredientes { get; set; }


        }
    }
}
