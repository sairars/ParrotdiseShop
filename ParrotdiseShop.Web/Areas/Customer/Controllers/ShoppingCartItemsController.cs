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
    }
}
