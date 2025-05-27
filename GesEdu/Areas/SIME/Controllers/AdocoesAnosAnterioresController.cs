using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class AdocoesAnosAnterioresController : BaseController
    {
        [Breadcrumb(Title = "Adoções anos anteriores")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
