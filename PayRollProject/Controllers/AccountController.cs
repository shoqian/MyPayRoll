using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;


namespace PayRollProject.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public IActionResult Login()
        {
            return View();
        }
    }
}