namespace PayRollProject.Controllers
{
	using Entities.Entities;
	using Entities.Models;
	using Microsoft.AspNetCore.Identity;

	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUsers> _userManager;
		private readonly SignInManager<ApplicationUsers> _signInManager;

		public AccountController(UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// GET
		[HttpGet]
		public IActionResult Login()
		{
			return User.Identity.IsAuthenticated ? Redirect($"/AdminPanel/Home/Index") : View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var findUser = await _userManager.FindByNameAsync(model.UserName);
				if (findUser == null)
				{
					ModelState.AddModelError("Password", "اطلاعات اول ورود صحیح نیست.");
					return View(model);
				}
				else
				{
					var result = await _signInManager
							.PasswordSignInAsync(
								model.UserName,
								model.Password,
								true,
								false);
					if (result.Succeeded)
					{

						return Redirect($"/AdminPanel/Home/Index");
					}
					else
					{
						ModelState.AddModelError("Password", "اطلاعات دوم ورود صحیح نیست.");
						return View(model);
					}
				}
			}

			return View(model);
		}

		public async Task<IActionResult> LogOut()
		{
			await this._signInManager.SignOutAsync();

			return Redirect($"/Account/Login");
		}
	}
}