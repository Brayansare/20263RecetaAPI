using _20263RecetaAPI.Models._20263RecetaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace _20263RecetaAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; } = default!;

        [StringLength(250)]
        public string? Description { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        //una categoria va a tener muchas recetas por eso se usa una lista icollection
        public ICollection<Receta> Recetas { get; set; }

    }
}
