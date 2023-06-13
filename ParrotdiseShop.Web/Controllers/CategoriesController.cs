using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
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
                IsEdit = true
            };

            return View("CategoryForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var categoryFromDb = _context.Categories.SingleOrDefault(c => c.Id == id);

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
                _context.Categories.Add(category);
            }
            else
            {
                var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == categoryDto.Id);

                if (categoryInDb == null)
                    return NotFound();

                _mapper.Map(categoryDto, categoryInDb);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
