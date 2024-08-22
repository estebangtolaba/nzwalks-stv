using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDTO
    {
        public string JwtToken { get; set; }
    }
}
