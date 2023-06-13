using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Core.ViewModels;
using ParrotdiseShop.Persistence.Data;
using System.Reflection;

namespace ParrotdiseShop.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
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
                IsEdit = true
            };

            return View("CategoryForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var categoryFromDb = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
                return NotFound();

            var categoryDto = new CategoryDto
            {
                Id = categoryFromDb.Id,
                Name = categoryFromDb.Name,
                DisplayOrder = categoryFromDb.DisplayOrder
            };

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
                _context.Categories.Add(new Category 
                            {
                                Id = categoryDto.Id,
                                Name = categoryDto.Name,
                                DisplayOrder = categoryDto.DisplayOrder
                            });
            else
            {
                var categoryFromDb = _context.Categories.Single(c => c.Id == categoryDto.Id);
                
                categoryFromDb.Name = categoryDto.Name;
                categoryFromDb.DisplayOrder = categoryDto.DisplayOrder;
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
