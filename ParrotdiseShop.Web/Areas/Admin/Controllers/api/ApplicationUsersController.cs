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
    }
}
