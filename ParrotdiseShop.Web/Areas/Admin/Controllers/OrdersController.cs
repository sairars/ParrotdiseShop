using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.Utilities;
using ParrotdiseShop.Core.ViewModels;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]
	public class OrdersController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
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

			var viewModel = new OrderViewModel
			{ 
				Order = _mapper.Map<OrderDto>(order),
				OrderDetails = _mapper.Map<IEnumerable<OrderDetailDto>>(_unitOfWork.OrderDetails.GetOrderDetailsWithProductBy(order.Id)),
				Provinces = CanadianProvinces.Provinces
			};

			if (User.IsInRole(RoleName.Customer))
				return View("ViewOrderDetails", viewModel);
			else
				return View("UpdateOrderDetails", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = $"{RoleName.Admin},{RoleName.Employee}")]
		public IActionResult UpdateCustomerInformation(OrderViewModel viewModel)
		{
			var orderFromDb = _unitOfWork.Orders.Get(o => o.Id == viewModel.Order.Id);

			if (orderFromDb == null)
				return NotFound();

			var order = _mapper.Map<Order>(viewModel.Order);
			orderFromDb.UpdateCustomerInformation(order);

			_unitOfWork.Complete();

			TempData["info"] = "Customer Information updated successfully";

			return RedirectToAction(nameof(Details), new { id = viewModel.Order.Id });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = $"{RoleName.Admin},{RoleName.Employee}")]
		public IActionResult Process(OrderViewModel viewModel)
		{
			var orderFromDb = _unitOfWork.Orders.Get(o => o.Id == viewModel.Order.Id);

			if (orderFromDb == null)
				return NotFound();

			orderFromDb.UpdateStatus(OrderStatus.StatusProcessing);
			_unitOfWork.Complete();

			return RedirectToAction(nameof(Details), new { id = viewModel.Order.Id });
		}

		[HttpPost]
		[Authorize(Roles = $"{RoleName.Admin},{RoleName.Employee}")]
		public IActionResult Ship(OrderViewModel viewModel)
		{
			var orderFromDb = _unitOfWork.Orders.Get(o => o.Id == viewModel.Order.Id);

			if (orderFromDb == null)
				return NotFound();

			orderFromDb.UpdateShippingInformation(viewModel.Order.Carrier, viewModel.Order.TrackingNumber);
			_unitOfWork.Complete();

			return RedirectToAction(nameof(Details), new { id = viewModel.Order.Id });
		}

		[HttpPost]
        [Authorize(Roles = $"{RoleName.Admin},{RoleName.Employee}")]
        public IActionResult Cancel(int id)
        {
            var orderFromDb = _unitOfWork.Orders.Get(o => o.Id == id);

            if (orderFromDb == null)
                return NotFound();

            orderFromDb.UpdateStatus(OrderStatus.StatusCancelled);
            _unitOfWork.Complete();

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
