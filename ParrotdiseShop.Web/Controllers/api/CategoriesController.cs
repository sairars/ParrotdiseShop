using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Web.Controllers.api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetCategoriesBySortedByDisplayOrder()
        {
            return Ok(_context.Categories.OrderBy(c => c.DisplayOrder));
        }
    }
}
