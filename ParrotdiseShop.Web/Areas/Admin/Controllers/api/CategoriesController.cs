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
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _unitOfWork.Categories.GetAll();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoryFromDb = _unitOfWork.Categories.Get(c => c.Id == id);

            if (categoryFromDb == null)
                return NotFound();

            _unitOfWork.Categories.Remove(categoryFromDb);
            _unitOfWork.Complete();

            return Ok("Category deleted successfully");
        }
    }
}
