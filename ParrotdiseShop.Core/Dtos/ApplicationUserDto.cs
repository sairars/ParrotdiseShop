
namespace ParrotdiseShop.Core.Dtos
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public string? Role { get; set; }
    }
}
