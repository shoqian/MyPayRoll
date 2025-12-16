namespace PayRollProject.Areas.AdminArea.Controllers
{
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Identity;
        using Microsoft.AspNetCore.Mvc;
        using PayRollProject.DataModel.Services.Interface;
        using PayRollProject.Entities.Entities;
        using Syncfusion.EJ2.Base;

        [Area("AdminArea")]
        [Authorize]
        public class CityManagementController : Controller
        {
                private readonly IUnitOfWork _context;
                private readonly IBaseTableRepository _repository;
                private readonly UserManager<ApplicationUsers> _userManager;

                public CityManagementController(IUnitOfWork context, IBaseTableRepository repository,
                        UserManager<ApplicationUsers> userManager)
                {
                        this._context = context;
                        this._repository = repository;
                        this._userManager = userManager;
                }

                // GET
                public IActionResult Index()
                {
                        var provinces = this._context.ProvincesUw
                                .Get(p => !p.IsDelete)
                                .Select(p => new { p.ProvinceId, p.ProvinceName })
                                .ToList();

                        ViewBag.Provinces = provinces;

                        return View();
                }

                public IActionResult FetchCityList([FromBody] DataManagerRequest dm, string mode = "")
                {
                        var cities = this._context.CitiesUw.Get(joinString: "Province").ToArray();
                        IEnumerable<CitiesTbl> dataSource;

                        dataSource = mode switch
                        {
                                "all" => cities,
                                "delete" => cities.Where(c => c.IsDelete),
                                _ => cities.Where(c => !c.IsDelete)
                        };

                        var op = new DataOperations();
                        if (dm.Search != null && dm.Search.Count > 0)
                        {
                                dataSource = op.PerformSearching(dataSource, dm.Search);
                        }

                        if (dm.Sorted != null && dm.Sorted.Count > 0)
                        {
                                dataSource = op.PerformSorting(dataSource, dm.Sorted);
                        }

                        if (dm.Where != null && dm.Where.Count > 0)
                        {
                                dataSource = op.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
                        }

                        var filteredCount = dataSource.Count();

                        if (dm.Skip != 0)
                        {
                                dataSource = op.PerformSkip(dataSource, dm.Skip);
                        }

                        if (dm.Take != 0)
                        {
                                dataSource = op.PerformTake(dataSource, dm.Take);
                        }

                        return dm.RequiresCounts
                                ? Json(new
                                {
                                        result = dataSource,
                                        action = "fetchGridCity",
                                        countAll = cities.Length,
                                        countDelete = cities.Count(p => p.IsDelete),
                                        count = filteredCount
                                })
                                : Json(dataSource);
                }

                public IActionResult Insert([FromBody] CRUDModel<CitiesTbl> model)
                {
                        try
                        {
                                var city = new CitiesTbl
                                {
                                        CityName = model.Value.CityName,
                                        Description = model.Value.Description,
                                        ProvinceID = model.Value.ProvinceID,
                                        IsDelete = false,
                                        CreateDateTime = DateTime.Now,
                                        UserID = this._userManager.GetUserId(this.HttpContext.User) ?? "System"
                                };

                                var duplicateCity = this._context.CitiesUw.Get(c => c.CityName == city.CityName
                                        && c.ProvinceID == city.ProvinceID);

                                if (duplicateCity.Any())
                                {
                                        return Json(new { action = "repeat", city = city.CityName });
                                }

                                this._context.CitiesUw.Create(city);
                                this._context.Save();
                                return Json(new { action = "insert", city = city.CityName });
                        }
                        catch (Exception e)
                        {
                                return Json(new { action = "error", ErrMsg = e.Message });
                        }
                }

                public IActionResult Update([FromBody] CRUDModel<CitiesTbl> model)
                {
                        try
                        {
                                _repository.UpdateCity(model);
                                return Json(new { action = "update", city = model.Value.CityName });
                        }
                        catch (Exception e)
                        {
                                return Json(new { action = "error", ErrMsg = e.Message });
                        }
                }

                [HttpPost]
                public IActionResult Delete([FromBody] CRUDModel<CitiesTbl> model)
                {
                        try
                        {
                                var key = model.Key.ToString();
                                var city = this._context.CitiesUw.GetById(int.Parse(key));
                                _repository.DeleteCity(int.Parse(key));
                                return Json(new { action = "delete", city = city?.CityName });
                        }
                        catch (Exception e)
                        {
                                return Json(new { action = "error", ErrMsg = e.Message });
                        }
                }

                [HttpPost]
                public IActionResult Restore([FromBody] CRUDModel<CitiesTbl> model)
                {
                        try
                        {
                                var key = model.Key.ToString();
                                var city = this._context.CitiesUw.GetById(int.Parse(key));
                                _repository.RestoreCity(int.Parse(key));
                                return Json(new { action = "restore", city = city?.CityName });
                        }
                        catch (Exception e)
                        {
                                return Json(new { action = "error", ErrMsg = e.Message });
                        }
                }
        }
}