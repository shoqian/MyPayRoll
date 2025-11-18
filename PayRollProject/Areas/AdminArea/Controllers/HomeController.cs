using Microsoft.AspNetCore.Mvc.Filters;

namespace PayRollProject.Areas.AdminArea.Controllers
{
	using Microsoft.AspNetCore.Authorization;

	[Area("AdminArea")]
    [Authorize]
    public class HomeController : Controller
    {
	    public HomeController()
	    {
	    }

	    public IActionResult Index()
        {
            return View();
        }

	    public override void OnActionExecuted(ActionExecutedContext context)
	    {
		    var areaName = this.ControllerContext.RouteData.Values["area"];
		    Console.WriteLine($"[DEBUG] Area: {areaName}");
		    base.OnActionExecuted(context);
	    }
    }
}
