using Microsoft.AspNetCore.Mvc;
using StorgoFirm.Web.ViewModels;
using System.Linq;

namespace StorgoFirm.Web.Controllers
{
    public class SportsController : Controller
    {
        [HttpGet]
        public IActionResult List(AppDatabase db)
        {
            EventSportViewModel[] viewModels = 
                db.Sports.Select(Map.ToViewModel).ToArray();

            return Json(viewModels);
        }
    }
}