using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.WebserviceModels.Adm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class HomeController : BaseController
    {
        [Breadcrumb(Title = "SIME-MEGA")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAnosLetivosSIME()
        {
            List<GetDominiosResponseItem> anosLetivosList =
            [
                new GetDominiosResponseItem 
                { 
                    codigo = User.GetAnoLetivoSIME(), 
                    descricao = User.GetAnoLetivoDescriptionSIME() 
                },
            ];

            if (!string.IsNullOrEmpty(User.GetAnoLetivoAnteriorSIME()))
                anosLetivosList.Add(new GetDominiosResponseItem
                {
                    codigo = User.GetAnoLetivoAnteriorSIME(),
                    descricao = User.GetAnoLetivoAnteriorDescriptionSIME()
                });

            return Json(anosLetivosList);
        }
    }
}
