using Microsoft.AspNetCore.Identity;

namespace DisneyAPI.Models
{
    public class Usuario : IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
