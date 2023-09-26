using AutoMapper;
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
                Products = _mapper.Map<IEnumerable<ProductDto>>(products),
                Categories = _mapper.Map<IEnumerable<CategoryDto>>(categories),
            };

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var productFromDb = _unitOfWork.Products.GetProductWithCategory(id);

            if (productFromDb == null)
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
        public IActionResult AddToCart(ShoppingCartItem shoppingCartItem)
        {
            var productFromDb = _unitOfWork.Products.GetProductWithCategory(shoppingCartItem.ProductId);

            if (productFromDb == null)
                return NotFound();

            shoppingCartItem.Product = productFromDb;

            if (shoppingCartItem.Product.UnitsInStock == 0)
                ModelState.AddModelError("", "Item is out of stock");
            else if (shoppingCartItem.Quantity > shoppingCartItem.Product.UnitsInStock)
                ModelState.AddModelError("Quantity", "This item is running low on stock. Please reduce quantity.");

            if(!ModelState.IsValid)
                return View(nameof(Details), shoppingCartItem);

            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.Get(sc => sc.UserId == userId
                                                                            && sc.ProductId == shoppingCartItem.ProductId);

                if (shoppingCartItemFromDb != null)
                    shoppingCartItemFromDb.Increment(shoppingCartItem.Quantity);
                else
                {
                    shoppingCartItem.UserId = userId;
                    shoppingCartItem.Created = DateTime.Now;
                    _unitOfWork.ShoppingCartItems.Add(shoppingCartItem);
                }
            }
            else
            {
                var guestCookieId = Request.Cookies["ShoppingCart"];
                
                if (guestCookieId == null)
                {
                    var options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    };

                    guestCookieId = Guid.NewGuid().ToString();
                    Response.Cookies.Append("ShoppingCart", guestCookieId, options);
                }
                
                var shoppingCartItemFromDb = _unitOfWork.ShoppingCartItems.Get(sc => sc.GuestCookieId == guestCookieId
                                                                            && sc.ProductId == shoppingCartItem.ProductId);

                if (shoppingCartItemFromDb != null)
                    shoppingCartItemFromDb.Increment(shoppingCartItem.Quantity);
                else
                {
                    shoppingCartItem.GuestCookieId = guestCookieId;
                    shoppingCartItem.Created = DateTime.Now;
                    _unitOfWork.ShoppingCartItems.Add(shoppingCartItem);
                }
            }

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