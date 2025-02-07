using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.MEGA.Controllers
{
    [Area("MEGA")]
    [Authorize(Policy = "MegaAccess")]
    public class HomeController : BaseController
    {
        [Breadcrumb(Title = "MEGA")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
