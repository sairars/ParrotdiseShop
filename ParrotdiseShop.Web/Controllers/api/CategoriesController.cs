using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
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
            var categoryInDb = _unitOfWork.Categories.Get(c => c.Id == id);

            if (categoryInDb == null)
                return NotFound();

            _unitOfWork.Categories.Remove(categoryInDb);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
