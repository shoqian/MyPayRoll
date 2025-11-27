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

		public ProvinceManagementController(IUnitOfWork context, IBaseTableRepository repository,
			UserManager<ApplicationUsers> userManager)
		{
			this._context = context;
			this._repository = repository;
			this._userManager = userManager;
		}


		// GET
		public IActionResult Index()
		{
			return View();
		}

                public IActionResult FetchProvinceList([FromBody] DataManagerRequest dm, string mode = "")
                {
                        var provinces = this._context.ProvincesUw.Get().ToArray();
                        IEnumerable<ProvinceTbl> dataSource;

                        if (mode == "all")
                        {
                                dataSource = provinces;
                        }
                        else if (mode == "delete")
                        {
                                dataSource = provinces.Where(p => p.IsDelete);
                        }
                        else
                        {
                                dataSource = provinces.Where(p => !p.IsDelete);
                        }

                        var op = new DataOperations();
                        if (dm.Search != null && dm.Search.Count > 0)
                        {
                                dataSource = op.PerformSearching(dataSource, dm.Search); // search
                        }

                        if (dm.Sorted != null && dm.Sorted.Count > 0)
                        {
                                dataSource = op.PerformSorting(dataSource, dm.Sorted); // sort
                        }

                        if (dm.Where != null && dm.Where.Count > 0)
                        {
                                dataSource = op.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator); // filter
                        }

                        var filteredCount = dataSource.Count();

                        if (dm.Skip != 0)
                        {
                                dataSource = op.PerformSkip(dataSource, dm.Skip); // paging: skip
                        }

                        if (dm.Take != 0)
                        {
                                dataSource = op.PerformTake(dataSource, dm.Take); // paging: take
                        }

                        return dm.RequiresCounts
                                ? Json(new
                                {
                                        result = dataSource,
                                        action = "fetchGridProvince",
                                        countAll = provinces.Length,
                                        countDelete = provinces.Count(p => p.IsDelete),
                                        count = filteredCount
                                })
                                : Json(dataSource);
                }

		public IActionResult Insert([FromBody] CRUDModel<ProvinceTbl> model)
		{
			try
			{
				var provinces = this._context.ProvincesUw.Get();
				ProvinceTbl province = new ProvinceTbl
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

		public IActionResult Update([FromBody] CRUDModel<ProvinceTbl> model)
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

		[HttpPost]
		public IActionResult Delete([FromBody] CRUDModel<ProvinceTbl> model)
		{
			try
			{
				var key = model.Key.ToString();
				var province = this._context.ProvincesUw.GetById(int.Parse(key));
				_repository.DeleteProvince(int.Parse(key));
				return Json(new { action = "delete", province = province.ProvinceName });
			}
			catch (Exception e)
			{
				return Json(new { action = "error", ErrMsg = e.Message });
			}
		}

		[HttpPost]
		public IActionResult Restore([FromBody] CRUDModel<ProvinceTbl> model)
		{
			try
			{
				var key = model.Key.ToString();
				var province = this._context.ProvincesUw.GetById(int.Parse(key));
				_repository.RestoreProvince(int.Parse(key));
				return Json(new { action = "restore", province = province.ProvinceName });
			}
			catch (Exception e)
			{
				return Json(new { action = "error", ErrMsg = e.Message });
			}
		}
	}
}