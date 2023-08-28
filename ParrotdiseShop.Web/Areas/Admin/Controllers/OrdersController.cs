using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = RoleName.Admin)]

	public class OrdersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
