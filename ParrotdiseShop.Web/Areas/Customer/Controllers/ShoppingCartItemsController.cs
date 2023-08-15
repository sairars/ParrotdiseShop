using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.ViewModels;
using System.Security.Claims;

namespace ParrotdiseShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartItemsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var userId = GetUserId();

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
        public IActionResult Checkout()
        {
            var userId = GetUserId();

            var shoppingCartItems = _unitOfWork.ShoppingCartItems.GetAllShoppingCartItemsWithProductsBy(userId);

            if (!shoppingCartItems.Any())
                return NotFound();

            var viewModel = new ShoppingCartViewModel
            {
                ShoppingCartItems = _mapper.Map<IEnumerable<ShoppingCartItemDto>>(shoppingCartItems)
            };

            if (!viewModel.IsValid)
                return RedirectToAction(nameof(Index));

            return View(viewModel);
        }

        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
