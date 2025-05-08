using GesEdu.Areas.SIME.Models;
using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Helpers.ExportPdf;
using GesEdu.Helpers.ExportPdf.Components.SIME;
using GesEdu.Helpers.ExportPdf.Models;
using GesEdu.Helpers.ExportPdf.Models.SIME;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IServices.SIME;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class NovasAdocoesController : BaseController
    {
        private readonly INovasAdocoesServices _novasAdocoesServices;
        private readonly PdfRenderer _pdfRenderer;

        public NovasAdocoesController(INovasAdocoesServices novasAdocoesServices, PdfRenderer pdfRenderer)
        {
            _novasAdocoesServices = novasAdocoesServices;
            _pdfRenderer = pdfRenderer;
        }

        [Breadcrumb(Title = "Novas adoções")]
        public async Task<IActionResult> Index()
        {
            var model = new NovasAdocoesViewModel()
            {
                Ciclos = await _novasAdocoesServices.GetCiclos() ?? [],
                Escolas = await _novasAdocoesServices.GetEscolas() ?? []
            };
            return View(model);
        }

        public async Task<IActionResult> ManuaisAdotadosPdfExport(string cod_escola_me)
        {
            var manuais = new GetManuaisAdotadosPdfModel
            {
                AnoLetivo = User.GetAnoLetivoDescription(),
                EnsinoRegular = await _novasAdocoesServices.GetManuaisAdotadosPdfExport(cod_escola_me, "REG"),
                EnsinoProfessional = await _novasAdocoesServices.GetManuaisAdotadosPdfExport(cod_escola_me, "EP"),
                CursosEducacaoFormacao = await _novasAdocoesServices.GetManuaisAdotadosPdfExport(cod_escola_me, "CEF")
            };

            var model = new PdfExportModel<GetManuaisAdotadosPdfModel>(manuais, HttpContext);

            var fileName = $"Manuais Adotados - Ano Letivo {User.GetAnoLetivoDescriptionSIME()}.pdf";
            var fileContentType = "application/pdf";
            var fileContent = await _pdfRenderer.RenderComponent<GetManuaisAdotadosPdf>(model.GetAsDictionary());

            return File(fileContent, fileContentType, fileName);
        }
    }
}
