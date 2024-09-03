using GesEdu.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace GesEdu.Areas.AreaReservada.Controllers
{
    [Area("AreaReservada")]
    [Authorize(Policy = "AreaReservadaAccess")]
    public class HomeController : BaseController
    {
        [Breadcrumb(FromController = typeof(GesEdu.Controllers.HomeController), FromAction = "Index", Title = "Área Reservada")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
