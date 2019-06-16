using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StorgoFirm.Extensions;
using StorgoFirm.Web.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace StorgoFirm.Web.Controllers
{
    [Route("api/exports")]
    public class ExportsController : Controller
    {
        [ApiPost]
        public IActionResult Upload([FromServices]AppDatabase db)
        {
            IFormFile jsonFile = Request.Form.Files.SingleOrDefault(
                x =>
                    x.Name == "export" &&
                    Path.HasExtension(x.FileName) &&
                    Path.GetExtension(x.FileName).Trim('.').ToLower() == "json"
            );

            if (jsonFile is null) return BadRequest(
                 error: "Request does not contain a single json file in 'export.'"
            );

            string fileContent;
            using (var reader = new StreamReader(jsonFile.OpenReadStream(), Encoding.UTF8))
            {
                fileContent = reader.ReadToEnd();
            }

            SportEventViewModel[] eventViewModels =
                JsonConvert.DeserializeObject<SportEventViewModel[]>(fileContent);

            Upload_ClearDatabase(db);
            Upload_InsertData(db, eventViewModels);
            db.SaveChanges();

            return Ok();
        }

        private static void Upload_ClearDatabase(AppDatabase db)
        {
            db.Sports.RemoveRange(db.Sports.ToArray());
            db.Leagues.RemoveRange(db.Leagues.ToArray());
            db.Events.RemoveRange(db.Events.ToArray());
        }

        private static void Upload_InsertData(AppDatabase db, SportEventViewModel[] eventViewModels)
        {
            EventSportViewModel[] sportVms = eventViewModels.Select(x => x.Sport).DistinctBy(x => x.Id).ToArray();
            var newSportIds = new Dictionary<ulong, long>();

            foreach (EventSportViewModel sportVm in sportVms)
            {
                Sport model = Map.ToModel(sportVm);
                long dbId = db.Sports.Add(model).Entity.Id;
                newSportIds.Add(sportVm.Id, dbId);
            }

            EventLeagueViewModel[] leagueVms = eventViewModels.Select(x => x.League).DistinctBy(x => x.Id).ToArray();
            var newLeagueIds = new Dictionary<ulong, long>();

            foreach (EventLeagueViewModel leagueVm in leagueVms)
            {
                League model = Map.ToModel(leagueVm);
                model.SportId = newSportIds[leagueVm.SportId];
                long dbId = db.Leagues.Add(model).Entity.Id;
                newLeagueIds.Add(leagueVm.Id, dbId);
            }

            foreach (SportEventViewModel eventVm in eventViewModels)
            {
                SportEvent model = Map.ToModel(eventVm);
                model.LeagueId = newLeagueIds[eventVm.League.Id];
                model.SportId = newSportIds[eventVm.Sport.Id];
                db.Events.Add(model);
            }
        }

        [ApiGet]
        public IActionResult Download([FromServices]AppDatabase db)
        {
            SportEvent[] models =
                db
                .Events
                .Include(x => x.Sport)
                .Include(x => x.League)
                .ToArray();

            SportEventViewModel[] viewModels = models.Select(Map.ToViewModel).ToArray();

            string jsonText =
                JsonConvert.SerializeObject(viewModels, Formatting.Indented);

            byte[] jsonTextBytes = Encoding.UTF8.GetBytes(jsonText);

            return File(
                jsonTextBytes, 
                contentType: MediaTypeNames.Application.Json, 
                fileDownloadName: "export.json"
            );
        }
    }
}