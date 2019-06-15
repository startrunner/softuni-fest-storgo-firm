using Microsoft.AspNetCore.Mvc;

namespace StorgoFirm.Web.Controllers
{
    public class ApiPostAttribute : HttpPostAttribute
    {
        public ApiPostAttribute() : base("[action]") { }
    }
}
