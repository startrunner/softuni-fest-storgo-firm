using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorgoFirm.Web.ViewModels;
using System;
using System.Globalization;
using System.Linq;

namespace StorgoFirm.Web.Controllers
{
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        [ApiGet]
        public IActionResult Get(
            [FromServices]AppDatabase db,
            [FromQuery, FromForm] long id
        )
        {
            SportEvent model =
                db
                .Events
                .Where(x => x.Id == id)
                .Include(x => x.Sport)
                .Include(x => x.League)
                .FirstOrDefault();

            if (model is null) return NotFound();

            SportEventViewModel viewModel = Map.ToViewModel(model);
            return new JsonResult(viewModel);
        }

        [ApiPost]
        public IActionResult UpdateNumbers(
            [FromServices]AppDatabase db,
            [FromBody]SportEventViewModel viewModel,
            [FromQuery, FromForm]long? id = null
        )
        {
            long queryId = id ?? (long)viewModel.Id;

            SportEvent model = db.Events.Where(x => x.Id == queryId).FirstOrDefault();

            if (model is null) return NotFound();

            model.AwayTeamOdds = viewModel.AwayTeamOdds;
            model.HomeTeamOdds = viewModel.HomeTeamOdds;
            model.DrawOdds = viewModel.DrawOdds;

            model.AwayTeamScore = viewModel.AwayTeamScore;
            model.HomeTeamScore = viewModel.HomeTeamScore;

            db.SaveChanges();

            return Ok();
        }

        [ApiGet]
        public int GetCount(
            [FromServices]AppDatabase db,
            [FromQuery, FromForm]long? sportId,
            [FromQuery, FromForm]long? leagueId
        )
        {
            IQueryable<SportEvent> query = db.Events;

            if (sportId != null) query = query.Where(x => x.Sport.Id == sportId.Value);
            if (leagueId != null) query = query.Where(x => x.LeagueId == leagueId.Value);

            return query.Count();
        }

        [ApiGet]
        public SportEventViewModel[] ListEarliest([FromServices]AppDatabase db, [FromQuery, FromForm] int count = 10)
        {
            SportEvent[] models =
                db
                .Events
                .OrderBy(x => x.DateUtc)
                .Include(x => x.Sport)
                .Include(x => x.League)
                .Take(count)
                .ToArray();

            SportEventViewModel[] viewModels = models.Select(Map.ToViewModel).ToArray();

            return viewModels;
        }

        [ApiGet]
        public SportEventViewModel[] List(
            [FromServices]AppDatabase db,
            [FromQuery, FromForm]string earliestDateUtc = null,
            [FromQuery, FromForm]long? timeWindowSeconds = null,
            [FromQuery, FromForm]string nameSearchString = null,
            [FromQuery, FromForm]long? sportId = null,
            [FromQuery, FromForm]long? leagueId = null
        )
        {
            if ((earliestDateUtc is null) != (timeWindowSeconds is null))
            {
                throw new Exception($"Both '{nameof(earliestDateUtc)}' and '{timeWindowSeconds}' must be set.");
            }


            IQueryable<SportEvent> query = db.Events;
            if (sportId != null) query = query.Where(x => x.SportId == sportId.Value);
            if (leagueId != null) query = query.Where(x => x.LeagueId == leagueId.Value);
            if (earliestDateUtc != null)
            {
                var earliestDateUtcParsed = DateTime.Parse(earliestDateUtc, CultureInfo.InvariantCulture);
                DateTime latestDateUtc = earliestDateUtcParsed.AddSeconds((double)timeWindowSeconds);
                query = query.Where(x => x.DateUtc >= earliestDateUtcParsed && x.DateUtc <= latestDateUtc);
            }
            if (nameSearchString != null)
            {
                query = query.Where(x => x.Name.Contains(nameSearchString, StringComparison.InvariantCultureIgnoreCase));
            }

            query =
                query
                .Include(x => x.Sport)
                .Include(x => x.League);

            SportEvent[] models = query.ToArray();
            SportEventViewModel[] viewModels = models.Select(Map.ToViewModel).ToArray();

            return viewModels;
        }
    }
}