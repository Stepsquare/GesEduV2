using GesEdu.Areas.SIME.Models;
using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Helpers.ExportPdf;
using GesEdu.Helpers.ExportPdf.Components.SIME;
using GesEdu.Helpers.ExportPdf.Models;
using GesEdu.Helpers.ExportPdf.Models.SIME;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.WebserviceModels.SIME;
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

        public async Task<IActionResult> ManuaisAdotadosCicloPartial(string ciclo)
        {
            var getEscolasCicloResponse = await _novasAdocoesServices.GetEscolasCiclos(ciclo) ?? [];

            var model = new ManuaisAdotadosCicloPartialViewModel()
            {
                ManuaisAdotados = await _novasAdocoesServices.GetManuaisAdotados(ciclo) ?? new(),
                TotalEscolasCiclo = getEscolasCicloResponse.Count()
            };

            return PartialView("_manuaisAdotadosCicloTabPartial", model);
        }

        public async Task<IActionResult> ManuaisAdotadosDetailModal(string ciclo, string ano_escolar, int id_disciplina)
        {
            var manuaisAdotadosResponse = await _novasAdocoesServices.GetManuaisAdotados(ciclo);

            var getEscolasCicloResponse = await _novasAdocoesServices.GetEscolasCiclos(ciclo) ?? [];

            var model = manuaisAdotadosResponse?.anos_escolares
                            .Where(a => a.ano_escolar == ano_escolar)
                            .SelectMany(a => a.disciplinas
                                .Where(d => d.id_disciplina == id_disciplina)
                                .Select(d => new ManuaisAdotadosDetailViewModel
                                {
                                    TotalEscolasCiclo = getEscolasCicloResponse.Count(),
                                    Manuais = d.manuais,
                                    AnoEscolar = a.ano_escolar,
                                    DisciplinaId = d.id_disciplina,
                                    DisciplinaDesc = d.des_disciplina
                                }))
                            .FirstOrDefault();

            return PartialView("_manuaisAdotadosModalPartial", model);
        }

        public async Task<IActionResult> NovaAdocaoModal(string ciclo, string ano_escolar, string id_disciplina)
        {
            var model = new NovaAdocaoModalViewModel
            {
                Escolas = await _novasAdocoesServices.GetEscolasCiclos(ciclo) ?? [],
                Manuais = await _novasAdocoesServices.GetManuaisEscola("ADO", ano_escolar, id_disciplina) ?? []
            };

            return PartialView("_novaAdocaoModalPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> SetManuaisEscola([FromBody] SetManuaisEscolaRequest requestObj)
        {
            return SuccessMessages(await _novasAdocoesServices.SetManuaisEscola(requestObj));
        }

        [HttpDelete]
        public async Task<IActionResult> DelManuaisEscola(int id_manual)
        {
            return SuccessMessages(await _novasAdocoesServices.DelManuaisEscola(id_manual));
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
