using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.AreaReservada.Controllers
{
    [Area("AreaReservada")]
    [Authorize(Policy = "AreaReservadaAccess")]
    public class HomeController : BaseController
    {
        [Breadcrumb(Title = "Área Reservada")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
