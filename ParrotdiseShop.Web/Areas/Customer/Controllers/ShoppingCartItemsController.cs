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

        public IActionResult UpdateOrRemoveShoppingCartItem(int id, int change)
        {
            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.Get(sc => sc.Id == id);

            if (shoppingCartItemFromDb == null)
                return NotFound();

            // Remove shopping Cart Item from Shopping Cart
            if (shoppingCartItemFromDb.Quantity + change == 0 || change == 0)
                _unitOfWork.ShoppingCartItems.Remove(shoppingCartItemFromDb);
            else
                shoppingCartItemFromDb.Update(change);
            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }
    }
}
