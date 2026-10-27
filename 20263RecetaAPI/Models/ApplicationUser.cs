using System.ComponentModel.DataAnnotations;
using _20263RecetaAPI.Models._20263RecetaAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace _20263RecetaAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(60)]
        public string? DisplayName { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Receta> Recetas { get; set; }
    }
}
