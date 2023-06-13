using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using ParrotdiseShop.Persistence.Data;

namespace ParrotdiseShop.Web.Controllers.api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult GetCategoriesBySortedByDisplayOrder()
        {
            var categories = _context.Categories;
            
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
    }
}
