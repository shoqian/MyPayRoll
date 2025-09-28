using System.Collections;
using PayRollProject.DataModel;
using PayRollProject.DataModel.ViewModel;
using PayRollProject.Entities.Models;
using Syncfusion.EJ2.Base;

namespace PayRollProject.Areas.AdminArea.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("AdminArea")]
    public class UserManagerController : Controller
    {
        private readonly PayRollDbContext _contex;
        private readonly IUserRepository _user;

        public UserManagerController(PayRollDbContext contex, IUserRepository user)
        {
            this._contex = contex;
            this._user = user;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FetchUserList([FromBody] DataManagerRequest dataManager)
        {
            IEnumerable dataSource = this._user.GetUserList();
            var dt = dataSource.Cast<UserListDTO>();
            int countAll = dt.Count();
            int countActive = dt.Count((UserListDTO u) => u.UserFlag == 1);
            int countDeActive = dt.Count((UserListDTO u) => u.UserFlag == 2);


            DataOperations operations = new DataOperations();

            if (dataManager.Search is { Count: > 0 })
            {
                dataSource = operations.PerformSearching(dataSource, dataManager.Search); // Searching جشتجوکردن
            }

            if (dataManager.Sorted is { Count: > 0 })
            {
                dataSource = operations.PerformSorting(dataSource, dataManager.Sorted); // Sorting مرتب سازی
            }

            if (dataManager.Where is { Count: > 0 })
            {
                dataSource = operations.PerformFiltering(dataSource, dataManager.Where,
                    dataManager.Where[0].Operator); // Filtering فیلترکردن
            }

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
                    action = "FetchGrid",
                    countAll = countAll,
                    countActive = countActive,
                    countDeactive = countDeActive
                })
                : Json(dataSource);
        }
    }
}