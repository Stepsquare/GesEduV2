using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.ISevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;

namespace GesEdu.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomepageServices _noticiasServices;

        public HomeController(IHomepageServices noticiasServices)
        {
            _noticiasServices = noticiasServices;
        }

        #region Views

        [DefaultBreadcrumb("Início")]
        public async Task<IActionResult> Index()
        {
            if (User.IsAdmin() && string.IsNullOrEmpty(User.GetCodigoServico()))
                return RedirectToAction("ChooseUo", "Authentication");

            var model = await _noticiasServices.GetNoticias();

            return View(model);
        }

        #endregion
    }
}