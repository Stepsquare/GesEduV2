using GesEdu.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class HomeController : BaseController
    {
        [Breadcrumb(FromController = typeof(GesEdu.Controllers.HomeController), FromAction = "Index", Title = "SIME")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
