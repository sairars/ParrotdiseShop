using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.ViewModels;
using System.Diagnostics;

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

        public IActionResult Index(int id)
        {
            var categories = _unitOfWork.Categories.GetAll();

            var products =  _unitOfWork.Products.GetProductsByCategory(id);

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

            var shoppingCartItemDto = _mapper.Map<ShoppingCartItemDto>(shoppingCartItem);

            var viewModel = new ShoppingCartViewModel
            {
                ShoppingCartItemDto = shoppingCartItemDto,
                Price = productFromDb.UnitPrice
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}