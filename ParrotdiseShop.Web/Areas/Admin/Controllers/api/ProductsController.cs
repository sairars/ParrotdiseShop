using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Utilities;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers.api
{
    [Route("/admin/api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = RoleName.Admin)]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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
                return NotFound("Item could not be deleted.");

            var fileDirectory = Path.Combine(_webHostEnvironment.WebRootPath, @"images\products");

            string fileName = Path.GetFileName(productFromDb.ImagePath);
            string filePath = Path.Combine(fileDirectory, fileName);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _unitOfWork.Products.Remove(productFromDb);
            _unitOfWork.Complete();

            return Ok("Product deleted successfully");
        }
    }
}
