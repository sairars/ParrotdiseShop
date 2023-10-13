using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParrotdiseShop.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }

        [NotMapped]
        public string? Role { get; set; }
    }
}
