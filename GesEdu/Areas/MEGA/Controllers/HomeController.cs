using GesEdu.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace GesEdu.Areas.MEGA.Controllers
{
    [Area("MEGA")]
    [Authorize(Policy = "MegaAccess")]
    public class HomeController : BaseController
    {
        [Breadcrumb(FromController = typeof(GesEdu.Controllers.HomeController), FromAction = "Index", Title = "MEGA")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
