using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.ViewModels;
using ParrotdiseShop.Persistence;
using static NuGet.Packaging.PackagingConstants;
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

        public OrdersController(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        [Route("{status}")]
        public IActionResult GetOrders(string status)
		{
			if (User.IsInRole(RoleName.Customer))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                return Ok(_unitOfWork.Orders.GetUserOrdersBy(status, userId));
            }

            return Ok(_unitOfWork.Orders.GetOrdersWithUserBy(status));
		}
	}
}
