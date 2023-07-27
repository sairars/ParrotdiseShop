using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers.api
{
    [Route("/admin/api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = RoleName.Admin)]
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var productFromDb = _unitOfWork.Products.Get(p => p.Id == id);

            if (productFromDb == null)
                return NotFound();

            _unitOfWork.Products.Remove(productFromDb);
            _unitOfWork.Complete();

            return Ok("Product deleted successfully");
        }
    }
}
