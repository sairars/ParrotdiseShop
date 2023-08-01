using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;
using System.Security.Claims;

namespace ParrotdiseShop.Web.Areas.Customer.Controllers.api
{
    [Route("/customer/api/[controller]")]
    [ApiController]
    [Area("Customer")]
    public class ShoppingCartItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartItemsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult GetShoppingCartItems()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCartItems =  _unitOfWork.ShoppingCartItems.GetAllShoppingCartItemsBy(userId);

            return Ok(_mapper.Map<IEnumerable<ShoppingCartItemDto>>(shoppingCartItems));

        }
    }
}
