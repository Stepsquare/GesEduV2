using GesEdu.Areas.SIME.Models;
using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using GesEdu.ServiceLayer.Services.SIME;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class ManuaisAdaptadosController : BaseController
    {
        private readonly IManuaisAdaptadosServices _manuaisAdaptadosServices;

        public ManuaisAdaptadosController(IManuaisAdaptadosServices manuaisAdaptadosServices)
        {
            _manuaisAdaptadosServices = manuaisAdaptadosServices;
        }

        [Breadcrumb(Title = "Manuais adaptados")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchAlunosAdaptados(GetAlunosManuaisAdaptadosParams searchParams)
        {
            var model = await _manuaisAdaptadosServices.GetAlunosManuaisAdaptados(searchParams);

            return PartialView("_manuaisAdaptadosSearchPartial", model);
        }

        public async Task<IActionResult> AdocaoManuaisAdaptadosModal(string tipoDoc, string numDoc)
        {
            var infoAluno = await _manuaisAdaptadosServices.GetIdentificaAlunoManAdapt(tipoDoc, numDoc) ?? throw new Exception("Aluno não encontrado.");

            var getManuaisAnoEscolarResponse = await _manuaisAdaptadosServices.GetManuaisAnoEscolar(infoAluno.id_aluno, infoAluno.ano_escolar);

            var model = new AdocaoAdaptadosModalViewModel
            {
                IdAluno = infoAluno.id_aluno,
                NomeAluno = infoAluno.nome,
                TipoDocAluno = infoAluno.tipo_doc_aluno,
                NumDocAluno = infoAluno.tipo_doc_aluno switch
                {
                    "CAC" => $"{infoAluno.num_doc_aluno} {infoAluno.cd_doc_aluno}",
                    _ => infoAluno.num_doc_aluno + infoAluno.cd_doc_aluno,
                },
                AnoEscolarAluno = infoAluno.ano_escolar,
                Manuais = getManuaisAnoEscolarResponse?.manuais?.Select(x => new AdocaoAdaptadosModalViewModel.Manual
                {
                    IdManual = x.id_manual,
                    IdManualEscola = x.id_manual_escola,
                    IdDisciplina = x.id_disciplina,
                    DescDisciplina = x.des_disciplina,
                    Isbn = x.isbn,
                    Titulo = x.titulo
                }).ToList()
            };

            return PartialView("_adocaoAdaptadosModalPartial", model);
        }

        public async Task<IActionResult> DetalheManuaisAdaptadosModal(string tipoDoc, string numDoc)
        {
            var infoAluno = await _manuaisAdaptadosServices.GetIdentificaAlunoManAdapt(tipoDoc, numDoc) ?? throw new Exception("Aluno não encontrado.");

            var getDetAlunosManAdaptadosResponse = await _manuaisAdaptadosServices.GetDetAlunosManAdaptados(infoAluno.id_aluno);

            var groupTipoManuais = getDetAlunosManAdaptadosResponse?.manuais_adaptados?
                                .GroupBy(manual => manual.tp_adaptacao)
                                .Select(group => group.Key)
                                .ToList();

            var groupManuais = getDetAlunosManAdaptadosResponse?.manuais_adaptados?.GroupBy(manual => manual.id_manual);

            var model = new AdocaoAdaptadosModalViewModel
            {
                IsReadOnly = true,
                IdAluno = infoAluno.id_aluno,
                NomeAluno = infoAluno.nome,
                TipoDocAluno = infoAluno.tipo_doc_aluno,
                NumDocAluno = infoAluno.tipo_doc_aluno switch
                {
                    "CAC" => $"{infoAluno.num_doc_aluno} {infoAluno.cd_doc_aluno}",
                    _ => infoAluno.num_doc_aluno + infoAluno.cd_doc_aluno,
                },
                AnoEscolarAluno = infoAluno.ano_escolar,
                TipoAdaptacao = groupTipoManuais?.Count switch
                {
                    2 => "Formato Digital e Braille",
                    1 => groupTipoManuais!.FirstOrDefault()!.Equals("BRA") ? "Formato Braille" : "Formato Digital",
                    _ => "N/A"
                },
                Manuais = groupManuais?.Select(group => new AdocaoAdaptadosModalViewModel.Manual
                {
                    IdManual = group.Key,
                    IdDisciplina = group.First().id_disciplina,
                    DescDisciplina = group.First().des_disciplina,
                    Isbn = group.First().isbn,
                    Titulo = group.First().titulo
                }).ToList()
            };

            return PartialView("_adocaoAdaptadosModalPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> SetAlunosManuaisAdaptados([FromBody] SetAlunosManuaisAdaptadosRequest requestObj)
        {
            return SuccessMessages(await _manuaisAdaptadosServices.SetAlunosManuaisAdaptados(requestObj));
        }
    }
}
