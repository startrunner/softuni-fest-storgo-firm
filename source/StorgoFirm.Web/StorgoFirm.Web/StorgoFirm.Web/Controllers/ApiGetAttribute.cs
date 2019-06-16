using Microsoft.AspNetCore.Mvc;

namespace StorgoFirm.Web.Controllers
{
    public class ApiGetAttribute : HttpGetAttribute
    {
        public ApiGetAttribute() : base("[action]") { }
    }
}
