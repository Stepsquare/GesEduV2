using GesEdu.Areas.SIME.Models;
using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Helpers.ExportPdf;
using GesEdu.ServiceLayer.Services.SIME;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class AdocoesAnosAnterioresController : BaseController
    {
        private readonly IAdocoesAnosAnterioresServices _adocoesAnosAnterioresServices;

        public AdocoesAnosAnterioresController(IAdocoesAnosAnterioresServices adocoesAnosAnterioresServices)
        {
            _adocoesAnosAnterioresServices = adocoesAnosAnterioresServices;
        }

        [Breadcrumb(Title = "Adoções anos anteriores")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchEstimativasManuais(GetManuaisAdotadosUoParams searchParams)
        {
            var result = await _adocoesAnosAnterioresServices.GetManuaisAdotadosUo(searchParams);

            var model = new SearchEstimativasManuaisViewModel
            {
                Results = await _adocoesAnosAnterioresServices.GetManuaisAdotadosUo(searchParams),
                IsReadOnly = User.GetAnoLetivoSIME() != searchParams.id_ano_letivo
            };

            return PartialView("_adocoesAnosAnterioresSearchPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> SetNumAlunos([FromBody] SetNumAlunosRequest requestObj)
        {
            return SuccessMessages(await _adocoesAnosAnterioresServices.SetNumAlunos(requestObj));
        }

        [HttpGet]
        public async Task<IActionResult> GetEscolas(string ano_letivo)
        {
            return Json(await _adocoesAnosAnterioresServices.GetEscolas(ano_letivo));
        }

        [HttpGet]
        public async Task<IActionResult> GetAnosEscolares(string ano_letivo)
        {
            return Json(await _adocoesAnosAnterioresServices.GetAnoEscolares("ADO", ano_letivo));
        }

        [HttpGet]
        public async Task<IActionResult> GetDisciplinasAnoEscolar(string ano_letivo, string ano_escolar, string tipologia)
        {
            return Json(await _adocoesAnosAnterioresServices.GetDisciplinas("ADO", ano_letivo, ano_escolar, tipologia));
        }
    }
}
