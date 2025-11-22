using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Entities.Entities;
using Syncfusion.EJ2.Base;

namespace PayRollProject.Areas.AdminArea.Controllers
{
	[Area("AdminArea")]
	[Authorize]
	public class ProvinceManagementController : Controller
	{
		private readonly IUnitOfWork _context;
		private readonly IBaseTableRepository _repository;

		private readonly UserManager<ApplicationUsers> _userManager;

		// GET
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult FetchProvinceList([FromBody] DataManagerRequest dm)
		{
			var dataSource = this._context.ProvincesUw.Get()
				.Where(p => p.IsDelete == false);
			var dt = dataSource.Cast<Province_Tbl>();
			var count = dt.Count();

			var op = new DataOperations();
			if (dm.Search != null && dm.Search.Count > 0)
			{
				dataSource = op.PerformSearching(dataSource, dm.Search); // جستجو
			}

			if (dm.Sorted != null && dm.Sorted.Count > 0)
			{
				dataSource = op.PerformSorting(dataSource, dm.Sorted); // مرتب کردن
			}

			if (dm.Where != null && dm.Where.Count > 0)
			{
				dataSource = op.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
			}

			if (dm.Skip != 0)
			{
				dataSource = op.PerformSkip(dataSource, dm.Skip);
			}

			if (dm.Take != 0)
			{
				dataSource = op.PerformTake(dataSource, dm.Take);
			}

			return dm.RequiresCounts
				? Json(new { result = dataSource, action = "fetchGridProvince", count = count })
				: Json(dataSource);
		}

		public IActionResult Insert([FromBody] CRUDModel<Province_Tbl> model)
		{
			try
			{
				var provinces = this._context.ProvincesUw.Get();
				Province_Tbl province = new Province_Tbl
				{
					ProvinceName = model.Value.ProvinceName,
					Description = model.Value.Description,
					CreateDateTime = DateTime.Now,
					IsDelete = false,
					UserID = this._userManager.GetUserId(this.HttpContext.User) ?? "System"
				};
				if (provinces.Any(p => p.ProvinceName == province.ProvinceName))
				{
					return Json(new { action = "repeat", province = province.ProvinceName });
				}
				else
				{
					this._context.ProvincesUw.Create(province);
					this._context.Save();
					return Json(new { action = "insert", province = province.ProvinceName });
				}
			}
			catch (Exception e)
			{
				return Json(new { action = "error", ErrMsg = e.Message });
			}
		}

		public IActionResult Update([FromBody] CRUDModel<Province_Tbl> model)
		{
			try
			{
				var provinces = this._context.ProvincesUw.Get();

				if (provinces.Any(p => p.ProvinceName == model.Value.ProvinceName))
				{
					return Json(new { action = "repeat", province = model.Value.ProvinceName });
				}
				else
				{
					_repository.UpdateProvince(model);
					return Json(new { action = "update", province = model.Value.ProvinceName });
				}
			}
			catch (Exception e)
			{
				return Json(new { action = "error", ErrMsg = e.Message });
			}
		}

		public IActionResult Delete([FromBody] CRUDModel<Province_Tbl> model)
		{
			try
			{
				_repository.DeleteProvince(model);
				return Json(new
				{
					action = "delete",
					province = model.Value.ProvinceName
				});
			}
			catch (Exception e)
			{
				return Json(new
				{
					action = "error",
					ErrMsg = e.Message
				});
			}
		}
	}
}