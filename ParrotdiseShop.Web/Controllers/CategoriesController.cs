using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Persistence.Data;

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
            var categoriesSortedByDisplayOrder = _context.Categories.OrderBy(c => c.DisplayOrder);
            return View(categoriesSortedByDisplayOrder);
        }

        public IActionResult Create()
        {
            return View("CategoryForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Category category)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", category);
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
