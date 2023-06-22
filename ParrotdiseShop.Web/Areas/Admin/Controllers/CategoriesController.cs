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
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            var viewModel = new CategoryFormViewModel
            {
                CategoryDto = new(),
                Heading = MethodBase.GetCurrentMethod().Name,
                IsEdit = false
            };

            return View("CategoryForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var categoryFromDb = _unitOfWork.Categories.Get(c => c.Id == id);

            if (categoryFromDb == null)
                return NotFound();

            var categoryDto = _mapper.Map<CategoryDto>(categoryFromDb);

            var viewModel = new CategoryFormViewModel
            {
                CategoryDto = categoryDto,
                Heading = MethodBase.GetCurrentMethod().Name,
                IsEdit = true
            };

            return View("CategoryForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CategoryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", viewModel);

            var categoryDto = viewModel.CategoryDto;


            if (categoryDto.Id == 0)
            {
                var category = _mapper.Map<Category>(categoryDto);
                _unitOfWork.Categories.Add(category);
                _unitOfWork.Complete();

                TempData["success"] = "Category created successfully";
            }
            else
            {
                var categoryFromDb = _unitOfWork.Categories.Get(c => c.Id == categoryDto.Id);

                if (categoryFromDb == null)
                    return NotFound();

                _mapper.Map(categoryDto, categoryFromDb);
                _unitOfWork.Complete();

                TempData["success"] = "Category updated successfully";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
