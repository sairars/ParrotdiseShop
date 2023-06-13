using Microsoft.AspNetCore.Mvc;
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
                Category = new(),
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

            var viewModel = new CategoryFormViewModel 
            {
                Category = categoryFromDb,
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

            var category = viewModel.Category;
            if (category.Id == 0) 
                _context.Categories.Add(viewModel.Category);
            else
            {
                var categoryFromDb = _context.Categories.SingleOrDefault(c => c.Id == category.Id);

                if (categoryFromDb == null)
                    return NotFound();
                
                categoryFromDb.Name = category.Name;
                categoryFromDb.DisplayOrder = category.DisplayOrder;
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
