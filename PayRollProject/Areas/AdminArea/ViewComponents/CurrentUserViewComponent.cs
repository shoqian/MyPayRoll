namespace PayRollProject.Areas.AdminArea.ViewComponents
{
	using Microsoft.AspNetCore.Identity;
	using PayRollProject.Entities.Entities;
	using ViewModels;

	public class CurrentUserViewComponent : ViewComponent
	{
		private readonly UserManager<ApplicationUsers> _userManager;

		public CurrentUserViewComponent(UserManager<ApplicationUsers> userManager)
		{
			this._userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = this._userManager.GetUserId(this.HttpContext.User);
			if (string.IsNullOrEmpty(userId)) return View(null);

			var user = await this._userManager.FindByIdAsync(userId);
			if (user == null) return View(null);

			var vm = new CurrentUserViewModel
			{
				Id = user.Id,
				FirstName = user.FirstName,
				Family = user.Family,
				Email = user.Email,
				IsActive = user.UserFlag != 2,
				PhoneNumber = user.PhoneNumber
			};
			return View(vm);
		}
	}
}