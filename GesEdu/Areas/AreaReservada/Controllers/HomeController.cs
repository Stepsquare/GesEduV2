using GesEdu.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.AreaReservada.Controllers
{
    [Area("AreaReservada")]
    [Authorize(Policy = "AreaReservadaAccess")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
