using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.ViewModels;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrdersController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Details(int id)
		{
			var order = _unitOfWork.Orders.GetOrderDetailsWithUser(id);
			
			if (order == null)
				return NotFound();

			var viewModel = new OrdersViewModel
			{
				Order = order,
				OrderDetails = _unitOfWork.OrderDetails.GetOrderDetailsBy(id)
			};

			if (User.IsInRole(RoleName.Customer))
				return View("Details", viewModel);
			else
				return View("EditOrderDetails", viewModel);
		}
	}
}
