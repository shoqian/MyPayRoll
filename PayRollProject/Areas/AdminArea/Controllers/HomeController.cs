namespace PayRollProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
