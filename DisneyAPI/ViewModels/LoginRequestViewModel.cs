using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Models
{
    public class LoginRequestViewModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
