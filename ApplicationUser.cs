using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _20263RecetaAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(60)]
        public string? DisplayName { get; set; }
        public DateTime Created { get; set; }
    }
}
