using GesEdu.Areas.SIME.Models;
using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Helpers.ExportPdf;
using GesEdu.Helpers.ExportPdf.Components.SIME;
using GesEdu.Helpers.ExportPdf.Models;
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
    public class ApreciacaoManuaisController : BaseController
    {
        private readonly IApreciacaoManuaisServices _apreciacaoManuaisServices;
        private readonly PdfRenderer _pdfRenderer;

        public ApreciacaoManuaisController(IApreciacaoManuaisServices apreciacaoManuaisServices, PdfRenderer pdfRenderer)
        {
            _apreciacaoManuaisServices = apreciacaoManuaisServices;
            _pdfRenderer = pdfRenderer;
        }

        [Breadcrumb(Title = "Apreciação de manuais escolares")]
        public async Task<IActionResult> Index()
        {
            var model = new ApreciacaoManuaisViewModel
            {
                Ciclos = await _apreciacaoManuaisServices.GetCiclos() ?? new List<GetCiclosUOResponseItem>()
            };

            return View(model);
        }

        public async Task<IActionResult> SearchManuaisApreciados(GetManuaisApreciadosParams searchParams)
        {
            var model = await _apreciacaoManuaisServices.GetManuaisApreciados(searchParams);

            return PartialView("_apreciacaoSearchPartial", model);
        }

        public async Task<IActionResult> ModalManualApreciadoDetailModal(string id_manual, string ano_letivo)
        {
            var model = await _apreciacaoManuaisServices.GetManualApreciado(id_manual, ano_letivo);

            return PartialView("_apreciacaoModalPartial", model);
        }

        public async Task<IActionResult> ManuaisPdfExport(string ano_letivo, string ciclo)
        {
            var manuais = await _apreciacaoManuaisServices.GetManuaisSIMEPdfExport(ano_letivo, ciclo) ?? throw new FileNotFoundException("Ficheiro não disponivel. Tente mais tarde.");
            
            var model = new PdfExportModel<GetManuaisSIMEResponse>(manuais, HttpContext);

            var fileName = $"Manuais - {manuais.ciclo} - Ano Letivo {manuais.ano_letivo}.pdf";
            var fileContentType = "application/pdf";
            var fileContent = await _pdfRenderer.RenderComponent<GetManuaisSIMEPdf>(model.GetAsDictionary());

            return File(fileContent, fileContentType, fileName);
        }

        [HttpPost]
        public async Task<IActionResult> SetManualApreciado([FromBody] SetManualAprDetRequest requestObj)
        {
            var responseMessage = await _apreciacaoManuaisServices.SetManualApreciado(requestObj);

            return SuccessMessage(responseMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnosEscolares(string ano_letivo)
        {
            return Json(await _apreciacaoManuaisServices.GetAnoEscolares(ano_letivo));
        }

        [HttpGet]
        public async Task<IActionResult> GetDisciplinasAnoEscolar(string ano_letivo, string ano_escolar, string tipologia)
        {
            return Json(await _apreciacaoManuaisServices.GetDisciplinas(ano_letivo, ano_escolar, tipologia));
        }
    }
}
