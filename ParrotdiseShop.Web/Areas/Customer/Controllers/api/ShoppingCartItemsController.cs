using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core;
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

        public ShoppingCartItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult GetNoOfItemsInShoppingCart()
        {
            IEnumerable<ShoppingCartItem> shoppingCartItems;

            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                TransferGuestShoppingCartToCustomerAccount(userId);

                shoppingCartItems = _unitOfWork.ShoppingCartItems
                                        .GetAllShoppingCartItemsWithProductsByUser(userId);
            }
            else
            {
                var guestCookieId = Request.Cookies["ShoppingCart"];
                shoppingCartItems = _unitOfWork.ShoppingCartItems
                                        .GetAllShoppingCartItemsWithProductsByCookie(guestCookieId);
            }

            return Ok(shoppingCartItems.Count());
        }

        private void TransferGuestShoppingCartToCustomerAccount(string userId)
        {
            var guestCookieId = Request.Cookies["ShoppingCart"];

            if (guestCookieId == null)
                return;

            var shoppingCartItems = _unitOfWork.ShoppingCartItems
                                        .GetAllShoppingCartItemsWithProductsByCookie(guestCookieId);
            foreach (var item in shoppingCartItems)
                item.UpdateUserId(userId);

            _unitOfWork.Complete();

            Response.Cookies.Delete("ShoppingCart");
        }
    }
}
