using Microsoft.AspNetCore.Mvc;
using StorgoFirm.Web.ViewModels;
using System.Linq;

namespace StorgoFirm.Web.Controllers
{
    [Route("api/[controller]")]
    public class LeaguesController : Controller
    {
        [HttpGet]
        public IActionResult List(AppDatabase db)
        {
            EventLeagueViewModel[] viewModels =
                db.Leagues.Select(Map.ToViewModel).ToArray().ToArray();

            return Json(viewModels);
        }
    }
}