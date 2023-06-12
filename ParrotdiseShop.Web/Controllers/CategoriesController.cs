using Microsoft.AspNetCore.Mvc;
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
    }
}
