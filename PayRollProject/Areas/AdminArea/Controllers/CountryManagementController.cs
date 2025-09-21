namespace PayRollProject.Areas.AdminArea.Controllers
{
    using System.Collections;
    using Entities.Entities;
    using Syncfusion.EJ2.Base;

    [Area("AdminArea")]
    public class CountryManagementController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IBaseTableRepository _repository;

        public CountryManagementController(IUnitOfWork context, IBaseTableRepository repository)
        {
            _context = context;
            this._repository = repository;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FetchCountryList([FromBody] DataManagerRequest dm)
        {
            IEnumerable dataSource = _context.CountriesUw.Get();
            var dt = dataSource.Cast<Countries>();
            int count = dt.Count();

            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search); // جستجو
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0)
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted); // مرتب کردن
            }

            if (dm.Where != null && dm.Where.Count > 0)
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator); // filtering
            }

            if (dm.Skip != 0)
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }

            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }

            return dm.RequiresCounts
                ? Json(new { result = dataSource, action = "fetchGrid", count = count })
                : Json(dataSource);
        }


        public IActionResult Insert([FromBody] CRUDModel<Countries> model)
        {
            try
            {
                var countries = this._context.CountriesUw.Get();
                Countries country = new Countries
                {
                    CountryName = model.Value.CountryName, Description = model.Value.Description
                };
                if (countries.Any((Countries c) => c.CountryName == country.CountryName))
                {
                    return Json(new { action = "repeat", country = country.CountryName });
                }
                else
                {
                    this._context.CountriesUw.Create(country);
                    this._context.Save();
                    return Json(new { action = "insert", country = country.CountryName });
                }
            }
            catch (Exception e)
            {
                return Json(new { action = "error", ErrMsg = e.Message });
            }
        }

        public IActionResult Update([FromBody] CRUDModel<Countries> model)
        {
            try
            {
                this._repository.UpdateCountry(model);

                return Json(new { action = "update", country = model.Value.CountryName });
            }
            catch (Exception e)
            {
                return Json(new { action = "error", ErrMsg = e.Message });
            }
        }

        public IActionResult Delete([FromBody] CRUDModel<Countries> model)
        {
            try
            {
                string? key = model.Key.ToString();
                var country = this._context.CountriesUw.GetById(Convert.ToInt32(key));
                this._context.CountriesUw.DeleteById(Convert.ToInt32(key));
                this._context.Save();
                return Json(new { action = "delete", country = country.CountryName });
            }
            catch (Exception e)
            {
                return Json(new { action = "error", ErrMsg = e.Message });
            }
        }
    }
}