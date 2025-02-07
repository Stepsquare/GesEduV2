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
        private readonly IHomepageServices _homepageServices;

        public HomeController(IHomepageServices homepageServices)
        {
            _homepageServices = homepageServices;
        }

        #region Views

        [Breadcrumb(Title ="Inicio")]
        public async Task<IActionResult> Index()
        {
            if ((User.IsAdmin() || User.IsSimeDgeUser()) && string.IsNullOrEmpty(User.GetCodigoServico()))
                return RedirectToAction("ChooseUo", "Authentication");

            var model = await _homepageServices.GetNoticias();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDominios(string code)
        {
            return Json(await _homepageServices.GetDominios(code));
        }
        #endregion
    }
}