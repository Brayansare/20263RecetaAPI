using System.Data;

namespace _20263RecetaAPI.DTOs.Identity
{
    public class AuthenticationResponseDto
    {
        public String Token { get; set; } = default!; // el default! es para decirle al compilador que no se preocupe por el valor nulo, ya que se inicializa en otro lugar
        public String Username { get; set; } = default!;
        public DateTime Expiration { get; set; } = default!;
        public string UserId { get; set; } = default!;

         

    }
}
