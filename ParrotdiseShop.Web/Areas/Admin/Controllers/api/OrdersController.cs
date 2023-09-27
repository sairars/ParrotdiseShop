using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Utilities;
using System.Security.Claims;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers.api
{
    [Route("/admin/api/[controller]")]
	[ApiController]
	[Area("Admin")]
	[Authorize]
	public class OrdersController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

        [Route("{status}")]
        public IActionResult GetOrders(string status)
		{
			IEnumerable<Order> orders;

			if (User.IsInRole(RoleName.Customer))
			{
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                orders = _unitOfWork.Orders.GetUserOrdersBy(status, userId);
			}
			else 
				orders = _unitOfWork.Orders.GetOrdersWithUserBy(status);

            
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }
	}
}
