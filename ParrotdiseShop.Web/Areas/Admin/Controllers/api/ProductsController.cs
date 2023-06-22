using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers.api
{
    [Route("/admin/api/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _unitOfWork.Products.GetAllProductsWithCategory();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }
    }
}
