using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Utilities;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers.api
{
    [Route("/admin/api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = RoleName.Admin)]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationUsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult GetAllUsers()
        {
            var applicationUsers = _unitOfWork.ApplicationUsers.GetAllUsersWithRole();

            return Ok(_mapper.Map<IEnumerable<ApplicationUserDto>>(applicationUsers));
        }

        [HttpPost("{id}")]
        public IActionResult LockUnlock(string id)
        {
            var currentusersId = User.GetUserId();

            // this api is only accessible by admin users
            // logged in admin user cannot lock/unlock themselves
            if (currentusersId.Equals(id))
                return BadRequest("Admin cannot lock their own account");

            var user = _unitOfWork.ApplicationUsers.Get(u => u.Id == id);

            if (user == null)
                return NotFound();

            // cannot lock/unlock a user for which this setting is disabled
            if (!user.LockoutEnabled)
                return BadRequest("Lock/unlock is unavailable for this user");

            user.SetLockOutEnd();

            _unitOfWork.Complete();

            return Ok("Operation successful");
        }
    }
}
