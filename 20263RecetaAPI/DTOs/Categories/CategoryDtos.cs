using System.ComponentModel.DataAnnotations;
//brayan 
namespace _20263RecetaAPI.DTOs.Categories
{
    public class CategoryDtos
    {//siempre pide un id 
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }

    public class CategoryCreationDtos
    {//las restricciones se pueden en aplicatiodbcontext o aqui nuestro modelo con nuestras propiedades
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; } = default!;//valor que puede ser nulo
        [StringLength(250)]
        public string? Description { get; set; }
    }

    public class CategoryUpdateDtos
    {
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; } = default!;
        [StringLength(250)]
        public string? Description { get; set; }
    } 
}
