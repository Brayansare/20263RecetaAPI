using System.ComponentModel.DataAnnotations;

namespace _20263RecetaAPI.DTOs.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Porciones { get; set; }
        public int Tiempo { get; set; }
        public string? Instrucciones { get; set; }
        public string? Descripcion { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public List<RecipeIngredientDtos> Ingredientes { get; set; } = new List<RecipeIngredientDtos>();
    }

    public class RecipeIngredientDtos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Cantidad { get; set; }
        public string? Medida { get; set; }
    }
    public class RecipeCreationDto
    {

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Porciones { get; set; }

        [Range(1, 1440)]
        public int Tiempo { get; set; }

        public string? Instrucciones { get; set; }
        public string? Descripcion { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        public List<RecipeIngredientCreationDtos> Ingredientes { get; set; } = new List<RecipeIngredientCreationDtos>();
    }
    public class RecipeIngredientCreationDtos
    {
        public int IdIngrediente { get; set; }
        public string? Cantidad { get; set; }
    }
    public class RecipeUpdateDtos
    {
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = default!;
        public int Porciones { get; set; }
        public int Tiempo { get; set; }
        public string? Instrucciones { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public int IdCategoria { get; set; }
        //se va a reutilizar el dro de creacion de ingredietes para actualizar
        public List<RecipeIngredientDtos> Ingredientes { get; set; } = new();

    }
}
