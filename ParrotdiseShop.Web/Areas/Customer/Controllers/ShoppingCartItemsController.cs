using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.ViewModels;
using Stripe.Checkout;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace ParrotdiseShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShoppingCartItemsController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			var shoppingCartItems = _unitOfWork.ShoppingCartItems
                                        .GetAllShoppingCartItemsWithProductsBy(userId);

            var viewModel = new ShoppingCartViewModel
            {
                ShoppingCartItems = _mapper.Map<IEnumerable<ShoppingCartItemDto>>(shoppingCartItems)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IncrementQuantity(int id)
        {
            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.GetShoppingCartItemWithProduct(id);

            if (shoppingCartItemFromDb == null)
                return NotFound();

            if (shoppingCartItemFromDb.Product.UnitsInStock == 0)
                ModelState.AddModelError("", 
                    $"{shoppingCartItemFromDb.Product.Name} is now out of stock. Please remove from cart to proceed to checkout");
            else if (shoppingCartItemFromDb.Quantity > shoppingCartItemFromDb.Product.UnitsInStock)
                ModelState.AddModelError("", 
                    $"{shoppingCartItemFromDb.Product.Name} is running low on stock. Please reduce quantity.");
            else if (shoppingCartItemFromDb.Quantity == shoppingCartItemFromDb.Product.UnitsInStock)
                ModelState.AddModelError("",
                    $"You have reached max. limit. Cannot add more quantity of {shoppingCartItemFromDb.Product.Name}");

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            shoppingCartItemFromDb.Increment(1);
            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DecrementQuantityOrRemove(int id)
        {
            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.Get(sc => sc.Id == id);

            if (shoppingCartItemFromDb == null)
                return NotFound();

            // Remove shopping Cart Item from Shopping Cart
            if (shoppingCartItemFromDb.Quantity == 1)
                _unitOfWork.ShoppingCartItems.Remove(shoppingCartItemFromDb);
            else
                shoppingCartItemFromDb.Decrement();

            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.Get(sc => sc.Id == id);

            if (shoppingCartItemFromDb == null)
                return NotFound();

            _unitOfWork.ShoppingCartItems.Remove(shoppingCartItemFromDb);
            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(ShoppingCartViewModel viewModel)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCartItems = _unitOfWork.ShoppingCartItems.GetAllShoppingCartItemsWithProductsBy(userId);

            if (!shoppingCartItems.Any())
                return NotFound();

            viewModel.ShoppingCartItems = _mapper.Map<IEnumerable<ShoppingCartItemDto>>(shoppingCartItems);

            if (!viewModel.IsValid)
                return RedirectToAction(nameof(Index));

            var customer = _unitOfWork.ApplicationUsers.Get(u => u.Id == userId);

            if (customer == null)
                return NotFound();

            viewModel.Order = _mapper.Map<OrderDto>(new Order(customer));
            viewModel.Provinces = CanadianProvinces.Provinces;
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(ShoppingCartViewModel viewModel)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCartItems = _unitOfWork.ShoppingCartItems
                                        .GetAllShoppingCartItemsWithProductsBy(userId);

            if (!shoppingCartItems.Any())
                return NotFound();

            viewModel.ShoppingCartItems = _mapper.Map<IEnumerable<ShoppingCartItemDto>>(shoppingCartItems);

            if (!viewModel.IsValid)
                return RedirectToAction(nameof(Index));

            viewModel.Order.UserId = userId;
            viewModel.Order.Total = viewModel.Total;
            viewModel.Order.Status = OrderStatus.StatusPending;
            viewModel.Order.PaymentStatus = OrderStatus.PaymentStatusPending;
            viewModel.Order.CreationDate = DateTime.Now;

            var order = _mapper.Map<Order>(viewModel.Order);

            _unitOfWork.Orders.Add(order);
            _unitOfWork.Complete();

            foreach (var item in viewModel.ShoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.UnitPrice
                };

                _unitOfWork.OrderDetails.Add(orderDetail);
            }

            _unitOfWork.Complete();

            return View("OrderConfirmation");
        }
    }
}
