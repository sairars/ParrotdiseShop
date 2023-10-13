using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Core.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetAllUsersWithRole();
    }
}
