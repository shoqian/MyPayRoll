using Microsoft.AspNetCore.Mvc;

namespace PayRollProject.Areas.AdminArea.Controllers
{
    using System.Collections;

    using PayRollProject.Entities.Entities;

    using Syncfusion.EJ2.Base;

    [Area("AdminArea")]
    public class CountryManagementController : Controller
    {
        private readonly IUnitOfWork _context;
        public CountryManagementController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FetchCountryList([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = _context.countriesUW.Get();
            var dt = DataSource.Cast<Countries>();
            int count = dt.Count();


            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);// جستجو
            }

            if (dm.Sorted != null && dm.Sorted.Count>0)
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted); // مرتب کردن
            }
            if (dm.Where != null && dm.Where.Count>0 )
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator); // filtering
            }
            if (dm.Sorted != null)
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip); // 
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }

            return dm.RequiresCounts ? Json(new
            {
                result = DataSource,
                action = "fetchgrid",
                count = count
            })
            : Json(DataSource);
        }

    }
}