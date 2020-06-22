using Microsoft.AspNetCore.Mvc;

namespace App.Core.IdentityServer4.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
