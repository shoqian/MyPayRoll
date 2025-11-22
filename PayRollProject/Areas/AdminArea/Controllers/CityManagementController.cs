using Microsoft.AspNetCore.Mvc;

namespace PayRollProject.Areas.AdminArea.Controllers
{
	public class CityManagementController : Controller
	{
		// GET
		public IActionResult Index()
		{
			return View();
		}
	}
}