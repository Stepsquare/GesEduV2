using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Shared.Interfaces.IServices.AreaReservada;
using GesEdu.Shared.SearchParams.AreaReservada;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GesEdu.Shared.WebserviceModels.AreaReservada.Cadastro;

namespace GesEdu.Areas.AreaReservada.Controllers
{
    [Area("AreaReservada")]
    [Authorize(Policy = "AreaReservadaAccess")]
    public class TrabalhadoresController : BaseController
    {
        private readonly ITrabalhadoresServices _trabalhadoresServices;

        public TrabalhadoresController(ITrabalhadoresServices trabalhadoresServices)
        {
            _trabalhadoresServices = trabalhadoresServices;
        }

        [Breadcrumb(Title = "Trabalhadores")]
        public IActionResult Listagem()
        {
            return View("~/Areas/AreaReservada/Views/Trabalhadores/Listagem/Index.cshtml");
        }

        public async Task<IActionResult> SearchTrabalhadores(GetFuncionariosUoRespParams filter)
        {
            var model = await _trabalhadoresServices.GetFuncionariosUoResp(filter);

            return PartialView("~/Areas/AreaReservada/Views/Trabalhadores/Listagem/_trabalhadoresSearchPartial.cshtml", model);
        }

        [Breadcrumb]
        public async Task<IActionResult> Detalhe(string nif)
        {
            var response = await _trabalhadoresServices.GetFuncionarioUoResp(nif);

            if (response == null)
                return RedirectToAction("Listagem");

            ViewData["BreadcrumbTitle"] = response?.nome;

            return View("~/Areas/AreaReservada/Views/Trabalhadores/Detalhe/Index.cshtml", response);
        }

        public async Task<IActionResult> HabilitacaoTrabalhadorPartial(string id_pessoa_unica)
        {
            var model = await _trabalhadoresServices.GetDocenteHabilitacao(id_pessoa_unica);

            return PartialView("~/Areas/AreaReservada/Views/Trabalhadores/Detalhe/_habilitacoesTrabalhadorTabPartial.cshtml", model);
        }

        public async Task<IActionResult> HabilitacaoTrabalhadorModalPartial(string id_pessoa_unica, int id_habilitacao)
        {
            var model = await _trabalhadoresServices.GetDocenteHabilitacao(id_pessoa_unica, id_habilitacao) ?? throw new Exception("Habilitação não encontrada.");

            return PartialView("~/Areas/AreaReservada/Views/Trabalhadores/Detalhe/_habilitacaoTrabalhadorModalPartial.cshtml", model);
        }

        public async Task<IActionResult> FormacaoTrabalhadorPartial(string id_pessoa_unica)
        {
            var model = await _trabalhadoresServices.GetDocenteFormacao(id_pessoa_unica);

            return PartialView("~/Areas/AreaReservada/Views/Trabalhadores/Detalhe/_formacoesTrabalhadorTabPartial.cshtml", model);
        }

        public async Task<IActionResult> FormacaoTrabalhadorModalPartial(string id_pessoa_unica, int id_formacao)
        {
            var model = await _trabalhadoresServices.GetDocenteFormacao(id_pessoa_unica, id_formacao) ?? throw new Exception("Formação não encontrada.");

            return PartialView("~/Areas/AreaReservada/Views/Trabalhadores/Detalhe/_formacaoTrabalhadorModalPartial.cshtml", model);
        }

        public async Task<IActionResult> GetAnexoPessoa(string id_anexo_pessoa)
        {
            var anexo = await _trabalhadoresServices.GetAnexoPessoa(id_anexo_pessoa) ?? throw new FileNotFoundException("Ficheiro não disponivel. Tente mais tarde.");

            byte[] fileBytes = Convert.FromBase64String(anexo.anexo_base64!);

            return File(fileBytes, anexo.mimetype!, anexo.filename);
        }

        [HttpPost]
        public async Task<IActionResult> SetDocenteEstadoHabilitacao([FromBody] SetDocenteEstadoHabilitacaoRequest requestObj)
        {
            var responseMessage = await _trabalhadoresServices.SetDocenteEstadoHabilitacao(requestObj);

            return SuccessMessage(responseMessage);
        }

        [HttpPost]
        public async Task<IActionResult> SetDocenteEstadoFormacao([FromBody] SetDocenteEstadoFormacaoRequest requestObj)
        {
            var responseMessage = await _trabalhadoresServices.SetDocenteEstadoFormacao(requestObj);

            return SuccessMessage(responseMessage);
        }
    }
}
