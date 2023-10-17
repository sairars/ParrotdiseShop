using Microsoft.EntityFrameworkCore;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Repositories;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Persistence.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllUsersWithRole()
        {
            // inner join aspnetuserroles with aspnetroles to get a list of userIds with 
            // their role details
            var userIdsWithRoles = _context.UserRoles
                                        .Join(_context.Roles,
                                                ur => ur.RoleId,
                                                r => r.Id,
                                                (ur, r) => new
                                                {
                                                    ur.UserId,
                                                    RoleId = r.Id,
                                                    RoleName = r.Name
                                                });

            // inner join application users with the above userIdsWithRoles and then
            // project into an applicationuser object with a necessary properties
            var applicationUsers = _context.ApplicationUsers
                                        .Join(userIdsWithRoles,
                                                u => u.Id,
                                                uidWithRoles => uidWithRoles.UserId,
                                                (u, uidWithRoles) => new
                                                {
                                                    u.Id,
                                                    u.Name,
                                                    u.Email,
                                                    u.PhoneNumber,
                                                    u.LockoutEnabled,
                                                    u.LockoutEnd,
                                                    uidWithRoles
                                                })
                                        .Select(au => new ApplicationUser()
                                        {
                                            Id = au.Id,
                                            Name = au.Name,
                                            Email = au.Email,
                                            PhoneNumber = au.PhoneNumber,
                                            LockoutEnabled = au.LockoutEnabled,
                                            LockoutEnd = au.LockoutEnd,
                                            Role = au.uidWithRoles.RoleName
                                        });

            return applicationUsers;
        }
    }
}
