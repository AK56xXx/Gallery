using Microsoft.AspNetCore.Identity;

namespace Gallery.Models.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
