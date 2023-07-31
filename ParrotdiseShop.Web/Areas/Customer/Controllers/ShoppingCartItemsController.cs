using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using System.Security.Claims;

namespace ParrotdiseShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartItemsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AddToCart(ShoppingCartItemDto shoppingCartItemDto)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.Get(sc => sc.UserId == userId 
                                                                            && sc.ProductId == shoppingCartItemDto.ProductId);

            if (shoppingCartItemFromDb != null)
                shoppingCartItemFromDb.Update(shoppingCartItemDto.Quantity);
            else
            {
                shoppingCartItemDto.UserId = userId;
                _unitOfWork.ShoppingCartItems.Add(_mapper.Map<ShoppingCartItem>(shoppingCartItemDto));
            }

            var product = _unitOfWork.Products.Get(p => p.Id == shoppingCartItemDto.ProductId);

            if (product == null)
                return NotFound();

            product.UpdateUnitsInStock(shoppingCartItemDto.Quantity);

            _unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }
    }
}
