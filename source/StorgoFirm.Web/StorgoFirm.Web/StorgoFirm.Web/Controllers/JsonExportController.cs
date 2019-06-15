using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StorgoFirm.Web.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace StorgoFirm.Web.Controllers
{
    public class JsonExportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload(IFormFile[] uploadedFiles)
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

            SportEventViewModel[] events =
                JsonConvert.DeserializeObject<SportEventViewModel[]>(fileContent);

            throw new NotImplementedException();
        }
    }
}