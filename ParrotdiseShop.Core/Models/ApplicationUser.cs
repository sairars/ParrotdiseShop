using Microsoft.AspNetCore.Identity;

namespace ParrotdiseShop.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
    }
}
