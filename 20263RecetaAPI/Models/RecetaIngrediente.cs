using _20263RecetaAPI.Models._20263RecetaAPI.Models;

namespace _20263RecetaAPI.Models
{
    public class RecetaIngrediente
    {
        public int Id { get; set; }

        //llaves foraneas
        public int IdReceta { get; set; }
        public int IdIngrediente { get; set; }

        public string? Cantidad { get; set; }

        // propiedades de navegacion
        public Receta? Receta { get; set; }
        public Ingrediente? Ingrediente { get; set; }
    }
}