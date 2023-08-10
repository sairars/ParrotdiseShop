using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
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
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCartItems = _unitOfWork.ShoppingCartItems.GetAllShoppingCartItemsWithProductsBy(userId).ToList();
            var total = shoppingCartItems
                            .Select(sc => new { TotalPerItem = sc.Quantity * sc.Product.UnitPrice })
                            .Sum(t => t.TotalPerItem);

            var viewModel = new ShoppingCartViewModel
            {
                ShoppingCartItems = _mapper.Map<IEnumerable<ShoppingCartItemDto>>(shoppingCartItems)
            };

            return View(viewModel);
        }

        public IActionResult IncrementQuantity(int id)
        {
            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.GetShoppingCartItemWithProduct(id);

            if (shoppingCartItemFromDb == null)
                return NotFound();

            var product = shoppingCartItemFromDb.Product;

            // Reduce units in stock and increase item quantity in shopping cart
            product.Decrement(1);
            shoppingCartItemFromDb.Increment(1);

            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DecrementQuantityOrRemove(int id)
        {
            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.GetShoppingCartItemWithProduct(id);

            if (shoppingCartItemFromDb == null)
                return NotFound();

            // Return item to product inventory - increment by 1
            var product = shoppingCartItemFromDb.Product;
            product.Increment(1);

            // Remove shopping Cart Item from Shopping Cart
            if (shoppingCartItemFromDb.Quantity == 1)
                _unitOfWork.ShoppingCartItems.Remove(shoppingCartItemFromDb);
            else
                shoppingCartItemFromDb.Decrement();

            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.GetShoppingCartItemWithProduct(id);

            if (shoppingCartItemFromDb == null)
                return NotFound();

            // Return item(s) to product inventory - increment by the quantity removed from shopping cart
            var product = shoppingCartItemFromDb.Product;
            product.Increment(shoppingCartItemFromDb.Quantity);

            _unitOfWork.ShoppingCartItems.Remove(shoppingCartItemFromDb);

            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            return View();
        }
    }
}
