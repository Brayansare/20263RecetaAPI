using System.ComponentModel.DataAnnotations;

namespace _20263RecetaAPI.DTOs.Cometarios
{
    public class CommentDtos
    {
        public int Id { get; set; }
        public string Texto { get; set; } = default!;
        public DateTime Creacion { get; set; }
        public string Usuario { get; set; } = default!;//solo nombre dle usuario

    }
    public class CommentCreationDtos
    {
        [Required]
        [StringLength(800)]
        public string Texto { get; set; } = default!;
        [Required]
        public int IdReceta { get; set; }

    }
    public class CommentUpdateDto
    {
        [Required]
        [StringLength(800)]
        public string Texto { get; set; } = default!;
    }
}
