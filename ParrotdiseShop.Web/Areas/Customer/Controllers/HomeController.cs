using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace ParrotdiseShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(int categoryId)
        {
            var categories = _unitOfWork.Categories.GetAll();

            var products =  _unitOfWork.Products.GetProductsBy(categoryId);

            var viewModel = new ProductsViewModel
            {
                ProductDtos = _mapper.Map<IEnumerable<ProductDto>>(products),
                CategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories),
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var productFromDb = _unitOfWork.Products.GetProductWithCategory(id);

            if (productFromDb == null || productFromDb.UnitsInStock < 1)
                return NotFound();

            var shoppingCartItem = new ShoppingCartItem
            {
                Product = productFromDb,
                Quantity = 1,
                ProductId = productFromDb.Id
            };

            return View(shoppingCartItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AddToCart(ShoppingCartItem shoppingCartItem)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.Get(sc => sc.UserId == userId
                                                                            && sc.ProductId == shoppingCartItem.ProductId);

            if (shoppingCartItemFromDb != null)
                shoppingCartItemFromDb.Update(shoppingCartItem.Quantity);
            else
            {
                shoppingCartItem.UserId = userId;
                _unitOfWork.ShoppingCartItems.Add(_mapper.Map<ShoppingCartItem>(shoppingCartItem));
            }

            var product = _unitOfWork.Products.Get(p => p.Id == shoppingCartItem.ProductId);

            if (product == null)
                return NotFound();

            product.UpdateUnitsInStock(shoppingCartItem.Quantity);

            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}