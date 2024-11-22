using GesEdu.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.MEGA.Controllers
{
    [Area("MEGA")]
    [Authorize(Policy = "MegaAccess")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
