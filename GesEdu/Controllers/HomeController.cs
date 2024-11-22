using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IHomepageServices _noticiasServices;

        public HomeController(IHomepageServices noticiasServices)
        {
            _noticiasServices = noticiasServices;
        }

        #region Views

        [Breadcrumb(Title ="Inicio")]
        public async Task<IActionResult> Index()
        {
            if ((User.IsAdmin() || User.IsSimeDgeUser()) && string.IsNullOrEmpty(User.GetCodigoServico()))
                return RedirectToAction("ChooseUo", "Authentication");

            var model = await _noticiasServices.GetNoticias();

            return View(model);
        }

        #endregion
    }
}