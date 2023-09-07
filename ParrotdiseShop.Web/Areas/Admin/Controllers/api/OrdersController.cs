using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.ViewModels;
using ParrotdiseShop.Persistence;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers.api
{
	[Route("/admin/api/[controller]")]
	[ApiController]
	[Area("Admin")]
	[Authorize(Roles = RoleName.Admin)]
	public class OrdersController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        [Route("{status}")]
        public IActionResult GetOrders(string status)
		{
            return Ok(_unitOfWork.Orders.GetOrdersWithUserBy(status));
		}
	}
}
