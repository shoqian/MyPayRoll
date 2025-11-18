using Microsoft.AspNetCore.Authorization;

namespace PayRollProject.Areas.AdminArea.Controllers
{
    public class CityManagementController1 : Controller
    {
        [Area("AdminArea")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
