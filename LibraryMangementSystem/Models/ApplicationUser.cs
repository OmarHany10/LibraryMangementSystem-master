using Microsoft.AspNetCore.Identity;

namespace LibraryMangementSystem.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? Address { get; set; }
    }
}
