using System.ComponentModel.DataAnnotations;

namespace _20263RecetaAPI.DTOs.Ingredients
{
    public class IngredientsDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Medida { get; set; }
    }
    public class IngredientCreationDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Medida { get; set; }
    }
    public class IngredientUpdateDto
    {
        [Required]
        public string Nombre { get; set; } = default!;
        [StringLength(250)]
        public string? Descripcion { get; set; }
        [StringLength(50)]
        public string? Medida { get; set; }
    }
}
