using Microsoft.AspNetCore.Mvc;
using StorgoFirm.Web.ViewModels;
using System.Linq;

namespace StorgoFirm.Web.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        [HttpGet]
        public IActionResult List(AppDatabase db)
        {
            SportEventViewModel[] viewModels =
                db.Events.Select(Map.ToViewModel).ToArray();

            return Json(viewModels);
        }
    }
}