using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StorgoFirm.Extensions;
using StorgoFirm.Web.ViewModels;
using System.IO;
using System.Linq;
using System.Text;

namespace StorgoFirm.Web.Controllers
{
    public class ExportsController : Controller
    {
        public IActionResult Upload(
            AppDatabase db,
            IFormFile[] uploadedFiles
        )
        {
            IFormFile jsonFile = uploadedFiles.SingleOrDefault(
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
            Sport[] sports =
                eventViewModels
                .Select(x => x.Sport)
                .DistinctBy(x => x.Id)
                .Select(Map.ToModel)
                .ToArray();

            League[] leagues =
                eventViewModels
                .Select(x => x.League)
                .DistinctBy(x => x.Id)
                .Select(Map.ToModel)
                .ToArray();

            SportEvent[] events =
                eventViewModels.Select(Map.ToModel).ToArray();

            db.Sports.AddRange(sports);
            db.Leagues.AddRange(leagues);
            db.Events.AddRange(events);
        }
    }
}