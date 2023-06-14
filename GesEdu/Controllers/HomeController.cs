using GesEdu.Shared.Interfaces.ISevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace GesEdu.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly INoticiasServices _noticiasServices;

        public HomeController(INoticiasServices noticiasServices)
        {
            _noticiasServices = noticiasServices;
        }

        #region Views

        [DefaultBreadcrumb]
        public async Task<IActionResult> Index()
        {
            var model = await _noticiasServices.GetNoticias();

            return View(model);
        }

        #endregion
    }
}