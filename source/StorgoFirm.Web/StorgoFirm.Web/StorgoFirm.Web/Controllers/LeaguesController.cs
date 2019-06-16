using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorgoFirm.Web.ViewModels;
using System.Linq;

namespace StorgoFirm.Web.Controllers
{
    [Route("api/leagues")]
    public class LeaguesController : Controller
    {
        [ApiGet]
        public IActionResult Get([FromServices]AppDatabase db, long id)
        {
            League model = db.Leagues.Where(x => x.Id == id).Include(x => x.Sport).FirstOrDefault();

            if (model is null) return NotFound();

            EventLeagueViewModel viewModel = Map.ToViewModel(model);

            return new JsonResult(viewModel);
        }

        [ApiGet]
        public EventLeagueViewModel[] List(
            [FromServices]AppDatabase db,
            [FromQuery, FromForm]string searchString,//will search by either name of leage or name of sport
            [FromQuery, FromForm]string sportNameSearchString,
            [FromQuery, FromForm]string nameSearchString,
            [FromQuery, FromForm]long? sportId
        )
        {
            IQueryable<League> query = db.Leagues;

            const System.StringComparison IgnoreCase = System.StringComparison.InvariantCultureIgnoreCase;

            if (sportId != null) query = query.Where(x => x.SportId == sportId.Value);
            if (nameSearchString != null)
            {
                query = query.Where(x => x.Name.Contains(nameSearchString, IgnoreCase));
            }
            if (sportNameSearchString != null)
            {
                query = query.Where(x => x.Sport.Name.Contains(sportNameSearchString, IgnoreCase));
            }
            if (searchString != null)
            {
                query = query.Where(x =>
                    x.Name.Contains(searchString, IgnoreCase) ||
                    x.Sport.Name.Contains(searchString, IgnoreCase)
                );
            }

            //query = query.Include(x => x.Sport);

            League[] models = query.ToArray();
            EventLeagueViewModel[] viewModels = models.Select(Map.ToViewModel).ToArray();

            return viewModels;
        }
    }
}