using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.ViewModels;
using System.Reflection;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            var categories = _unitOfWork.Categories.GetAll();

            var viewModel = new ProductFormViewModel
            {
                CategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories),
                Heading = MethodBase.GetCurrentMethod().Name,
                IsEdit = false
            };

            return View("ProductForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var productFromDb = _unitOfWork.Products.Get(c => c.Id == id);

            if (productFromDb == null)
                return NotFound();

            var categories = _unitOfWork.Categories.GetAll();
            var viewModel = new ProductFormViewModel
            {
                ProductDto = _mapper.Map<ProductDto>(productFromDb),
                CategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories),
                Heading = MethodBase.GetCurrentMethod().Name,
                IsEdit = true
            };

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ProductFormViewModel viewModel, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                var categories = _unitOfWork.Categories.GetAll();
                viewModel.CategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                return View("ProductForm", viewModel);
            }

            var productDto = viewModel.ProductDto;

            if (file != null)
            {
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\products");

                if (productDto.ImagePath != null)
                {
                    string oldFileName = Path.GetFileName(productDto.ImagePath);
                    string oldFilePath = Path.Combine(uploadPath, oldFileName);

                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);
                }

                var newFileName = Guid.NewGuid().ToString();

                var extension = Path.GetExtension(file.FileName);

                using var fileStream = new FileStream(Path.Combine(uploadPath, newFileName + extension), FileMode.Create);
                file.CopyTo(fileStream);

                productDto.ImagePath = @"\images\products\" + newFileName + extension;
            }

            if (productDto.Id == 0)
            {
                _unitOfWork.Products.Add(_mapper.Map<Product>(productDto));
                _unitOfWork.Complete();

                TempData["success"] = "Product created successfully";
            }
            else
            {
                var productFromDb = _unitOfWork.Products.Get(p => p.Id == productDto.Id);

                if (productFromDb == null)
                    return NotFound();

                _mapper.Map(productDto, productFromDb);
                
                _unitOfWork.Complete();

                TempData["success"] = "Product updated successfully";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
