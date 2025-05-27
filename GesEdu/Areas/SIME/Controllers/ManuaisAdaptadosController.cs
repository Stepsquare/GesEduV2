using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class ManuaisAdaptadosController : BaseController
    {
        [Breadcrumb(Title = "Manuais adaptados")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
