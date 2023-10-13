using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core.Utilities;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleName.Admin)]
    public class ApplicationUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
