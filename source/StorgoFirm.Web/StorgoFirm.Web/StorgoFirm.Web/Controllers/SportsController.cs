using Microsoft.AspNetCore.Mvc;
using StorgoFirm.Web.ViewModels;
using System;
using System.Linq;

namespace StorgoFirm.Web.Controllers
{
    [Route("api/[controller]")]
    public class SportsController : ControllerBase
    {
        [ApiGet]
        public IActionResult Get([FromServices]AppDatabase db, long id)
        {
            Sport model = db.Sports.Where(x => x.Id == id).FirstOrDefault();
            if (model is null) return NotFound();

            EventSportViewModel viewModel = Map.ToViewModel(model);

            return new JsonResult(viewModel);
        }

        [ApiGet]
        public EventSportViewModel[] List(
            [FromServices]AppDatabase db,
            [FromQuery, FromForm]string nameSearchString = null
        )
        {
            IQueryable<Sport> query = db.Sports;

            if (nameSearchString != null)
            {
                query = query.Where(x => x.Name.Contains(nameSearchString, StringComparison.InvariantCultureIgnoreCase));
            }

            Sport[] models = query.ToArray();

            EventSportViewModel[] viewModels = models.Select(Map.ToViewModel).ToArray();
            return viewModels;
        }
    }
}