// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManagerController.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UserManagerController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.SignalR;

namespace PayRollProject.Areas.AdminArea.Controllers
{
	using System.Collections;
	using Entities.Entities;
	using Entities.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Syncfusion.EJ2.Base;

	[Area("AdminArea")]
	[Authorize]
	public class UserManagerController : Controller
	{
		private readonly IUnitOfWork _context;
		private readonly IUserRepository _user;
		private readonly UserManager<ApplicationUsers> _userManager;

		public UserManagerController(IUnitOfWork context, IUserRepository user,
			UserManager<ApplicationUsers> userManager)
		{
			_context = context;
			_user = user;
			_userManager = userManager;
		}

		// GET
		// [HttpGet]
		// public async Task<IActionResult> Index()
		// {
		// 	var userId = _userManager.GetUserId(User);
		// 	object currentUserVm;
		// 	if (userId != null)
		// 	{
		// 		var user = await _userManager.FindByIdAsync(userId);
		// 		if (user != null)
		// 		{
		// 			currentUserVm = new
		// 			{
		// 				user.Id,
		// 				user.FirstName,
		// 				user.Family,
		// 				user.Email,
		// 				user.PhoneNumber
		// 			};
		// 			this.ViewData["CurrentUserVm"] = currentUserVm;
		// 		}
		// 	}
		//
		// 	return View();
		// }
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		// public async Task<IActionResult> GetCurrentUser()
		// {
		// 	var userId = this._userManager.GetUserId(this.User);
		// 	if (userId == null) return Json(null);
		//
		// 	var user = await this._userManager.FindByIdAsync(userId);
		// 	if (user == null) return Json(null);
		//
		// 	var vm = new
		// 	{
		// 		user.Id,
		// 		user.FirstName,
		// 		user.Family,
		// 		user.Email,
		// 		user.PhoneNumber
		// 	};
		// 	return Json(vm);
		// }

		public IActionResult FetchUserList([FromBody] DataManagerRequest dataManager)
		{
			IEnumerable dataSource = this._user.GetUserList();
			var all = dataSource.Cast<UserListDTO>();
			int countAllUser = all.Count();
			int countActiveUser = all.Count((UserListDTO u) => u.UserFlag == 1);
			int countDeactivateUser = all.Count((UserListDTO u) => u.UserFlag == 2);


			DataOperations operations = new DataOperations();

			if (dataManager.Search != null && dataManager.Search.Count > 0)
			{
				dataSource = operations.PerformSearching(dataSource, dataManager.Search); // Searching جشتجوکردن
			}

			if (dataManager.Sorted?.Count > 0)
			{
				dataSource = operations.PerformSorting(dataSource, dataManager.Sorted); // Sorting مرتب سازی
			}

			if (dataManager.Where?.Count > 0)
			{
				dataSource =
					operations.PerformFiltering(dataSource, dataManager.Where,
						dataManager.Where[0].Operator); // Filtering فیلترکردن
			}

			var filteredCount = dataSource.Cast<UserListDTO>().Count();

			if (dataManager.Skip != 0)
			{
				dataSource = operations.PerformSkip(dataSource, dataManager.Skip);
			}

			if (dataManager.Take != 0)
			{
				dataSource = operations.PerformTake(dataSource, dataManager.Take);
			}

			return dataManager.RequiresCounts
				? Json(new
				{
					result = dataSource,
					action = "fetchGrid",
					count = filteredCount,
					countAll = countAllUser,
					countActive = countActiveUser,
					countDeactivate = countDeactivateUser
				})
				: Json(dataSource);
		}

		public async Task<IActionResult> Insert([FromBody] CRUDModel<UserListDTO> model)
		{
			try
			{
				var getUser = this._context.UserManager
					.Get((ApplicationUsers u) => u.UserName == model.Value.MelliCode);
				if (getUser.Any())
				{
					// تکراری
					return Json(new
					{
						action = "repeatUser",
						user = model.Value.FirstName + " " + model.Value.Family,
						melli = model.Value.MelliCode
					});
				}

				var newUser = new ApplicationUsers
				{
					FirstName = model.Value.FirstName,
					Family = model.Value.Family,
					PhoneNumber = model.Value.PhoneNumber,
					Email = model.Value.Email,
					MelliCode = model.Value.MelliCode,
					UserName = model.Value.MelliCode,
					Gender = Convert.ToByte(model.Value.GenderText),
					UserType = Convert.ToByte(model.Value.UserTypeText),
					UserFlag = 1 // default is Active
				};

				// ایجاد کاربر
				IdentityResult result = await this._userManager.CreateAsync(newUser, "123456");
				if (result.Succeeded)
				{
					// تنظیم نقش کاربر
					if (model.Value.UserTypeText == "1")
					{
						// کاربر ادمین
						await this._userManager.AddToRoleAsync(newUser, "adminPanel");
					}
					else if (model.Value.UserTypeText == "2")
					{
						// کاربر عادی
						await this._userManager.AddToRoleAsync(newUser, "userPanel");
					}
				}

				this._context.SaveAsync();
				return Json(new { action = "insert", user = newUser.FirstName + " " + newUser.Family });
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Json(new { action = "error", ErrMsg = e.Message, text = e.Data });
			}
		}

		public async Task<IActionResult> Update([FromBody] CRUDModel<UserListDTO> model)
		{
			try
			{
				var currentUser = await this._userManager.FindByIdAsync(model.Value.Id);
				if (currentUser == null)
				{
					return Json(new { action = "notFound" });
				}

				currentUser.FirstName = model.Value.FirstName;
				currentUser.Family = model.Value.Family;
				currentUser.PhoneNumber = model.Value.PhoneNumber;
				currentUser.Email = model.Value.Email;
				currentUser.Gender = Convert.ToByte(model.Value.GenderText);


				IdentityResult result = await this._userManager.UpdateAsync(currentUser);
				if (result.Succeeded)
				{
					return Json(new { action = "update", user = currentUser.FirstName + " " + currentUser.Family });
				}
				else
				{
					return Json(new
					{
						action = "error",
						ErrMsg = "ظاهرا خطایی رخ داده لطفا با ادمین تماس بگیرید",
						text = "خطای مربوط به ذخیره سازی در سرور"
					});
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Json(new { action = "error", ErrMsg = e.Message, text = e.Data });
			}
		}

		[HttpPost]
		public async Task<IActionResult> DeactivateUser([FromBody] CRUDModel<UserListDTO> model)
		{
			_user.DeactivateUser(model.Value.MelliCode);
			if (!this.ModelState.IsValid)
			{
				return Json(new
				{
					action = "error",
					ErrMsg = "خطا در ارتباط با فعال  و غیر فعال کردن",
					text = "متاسفانه نتوانستیم کاربر مورد نظر را غیر فعال کنیم."
				});
			}

			return Json(new { action = "deactivate", user = model.Value.FirstName + " " + model.Value.Family });
		}

		[HttpPost]
		public async Task<IActionResult> ActiveUser([FromBody] CRUDModel<UserListDTO> model)
		{
			this._user.ActiveUser(model.Value.MelliCode);
			if (!this.ModelState.IsValid)
			{
				return Json(new
				{
					action = "error",
					ErrMsg = "خطا در ارتباط با فعال  و غیر فعال کردن",
					text = "متاسفانه نتوانستیم کاربر مورد نظر را غیر فعال کنیم."
				});
			}

			return Json(new { action = "deactivate", user = model.Value.FirstName + " " + model.Value.Family });
		}
	}
}