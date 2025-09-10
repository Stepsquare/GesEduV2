using GesEdu.Areas.SIME.Models;
using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Helpers.ExportPdf;
using GesEdu.Helpers.ExportPdf.Components.SIME;
using GesEdu.Helpers.ExportPdf.Models;
using GesEdu.Helpers.ExportPdf.Models.SIME;
using GesEdu.ServiceLayer.Services.SIME;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IServices.SIME;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels.SIME;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class PedidosExcecaoController : BaseController
    {

        private readonly IPedidosExcecaoServices _pedidosExcecaoServicesServices;
        private readonly PdfRenderer _pdfRenderer;

        public PedidosExcecaoController(IPedidosExcecaoServices pedidosExcecaoServicesServices, PdfRenderer pdfRenderer)
        {
            _pedidosExcecaoServicesServices = pedidosExcecaoServicesServices;
            _pdfRenderer = pdfRenderer;
        }

        #region Listagem Pedidos

        [Breadcrumb(Title = "Pedidos de exceção")]
        public async Task<IActionResult> Index()
        {
            var getDominosResponse = await _pedidosExcecaoServicesServices.GetDominios("EXCECAO_SIME") ?? [];

            var model = new PedidosExcecaoListagemViewModel
            {
                TiposExcecao = getDominosResponse.OrderBy(x => x.descricao_full).ToList()
            };

            return View("Listagem/Index", model);
        }

        public async Task<IActionResult> SearchPedidosExcecao(GetPedidosExcecaoParams searchParams)
        {
            var model = await _pedidosExcecaoServicesServices.GetPedidosExcecao(searchParams);

            return PartialView("Listagem/_pedidosExcecaoSearchPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> SetPedExcecao([FromBody] SetPedExcecaoRequest requestObj, [FromHeader] bool goDetail)
        {
            var response = await _pedidosExcecaoServicesServices.SetPedExcecao(requestObj);

            if (goDetail)
                return SuccessRedirect(Url.Action("Detalhe", "PedidosExcecao", new
                {
                    idPedido = response?.id_pedido_cab,
                    anoLetivo = User.GetAnoLetivoSIME()
                }));

            return SuccessMessages(response?.messages);
        }

        #endregion


        #region Detalhe

        [Breadcrumb(ResetAreaNodes = false)]
        public async Task<IActionResult> Detalhe(string idPedido, string anoLetivo)
        {
            ViewData["BreadcrumbTitle"] = $"Pedido #{idPedido}";

            var model = new PedidoExcecaoDetalheViewModel
            {
                Detalhe = await _pedidosExcecaoServicesServices.GetPedExcecao(idPedido, anoLetivo) ?? throw new Exception("Pedido de exceção não encontrado."),
                TiposAnexo = await _pedidosExcecaoServicesServices.GetDominios("SIME_ANEXO") ?? [],
            };

            model.AnosEscolaridade = await _pedidosExcecaoServicesServices.GetAnoEscolares("EXE", anoLetivo, model.Detalhe.tipo_ensino) ?? [];

            return View("Detalhe/Index", model);
        }

        public async Task<IActionResult> ModalAdocaoManualExcecaoPartial(string idPedido, string tipoExcecao, string idDisciplina, string codAgrDisc, string ciclo, string anoEscolar)
        {
            var model = new AdotarManualPedidoPartialViewModel
            {
                Escolas = await _pedidosExcecaoServicesServices.GetEscolasCiclos(ciclo) ?? [],
                IdPedido = idPedido
            };

            if (tipoExcecao == "3")
                model.Manuais = await _pedidosExcecaoServicesServices.GetManuaisEscolaPLNM(codAgrDisc, anoEscolar) ?? [];
            else
                model.Manuais = await _pedidosExcecaoServicesServices.GetManuaisEscola("EXE", anoEscolar, idDisciplina) ?? [];

            return PartialView("Detalhe/_adotarManualPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdPedExcecao([FromBody] UpdPedExcecaoRequest requestObj)
        {
            var pedido = await _pedidosExcecaoServicesServices.GetPedExcecao(requestObj.id_pedido_cab.ToString()) ?? throw new Exception("Pedido de exceção não encontrado.");

            if (requestObj.justif_pedido is null)
            {
                requestObj.justif_pedido = pedido.justif_pedido;
            }
           
            requestObj.id_excecao = pedido.id_excecao;

            return SuccessMessages(await _pedidosExcecaoServicesServices.UpdPedExcecao(requestObj));
        }

        [HttpDelete]
        public async Task<IActionResult> DelManualExcecao([FromBody] DelManualExcecaoRequest requestObj)
        {
            return SuccessMessages(await _pedidosExcecaoServicesServices.DelManualExcecao(requestObj));
        }

        public async Task<IActionResult> GetAnexoPed(string idPedido, string idAnexo, string tipoAnexo)
        {
            var anexo = await _pedidosExcecaoServicesServices.GetAnexoPed(idPedido, idAnexo, tipoAnexo) ?? throw new FileNotFoundException("Ficheiro não disponivel. Tente mais tarde.");

            byte[] fileBytes = Convert.FromBase64String(anexo.anexo_base64!);

            return File(fileBytes, anexo.mimetype!, anexo.filename);
        }

        public async Task<IActionResult> DeclaracoesPdfExport(string idPedido, string anoLetivo)
        {
            var detalhePedido = await _pedidosExcecaoServicesServices.GetPedExcecao(idPedido, anoLetivo) ?? throw new FileNotFoundException("Ficheiro não disponivel. Tente mais tarde."); ;

            var model = new PdfExportModel<GetPedExcecaoResponse>(detalhePedido, HttpContext);

            var fileName = $"Pedido #{detalhePedido.id_pedido_cab} - {detalhePedido.des_excecao}.pdf";
            var fileContentType = "application/pdf";
            var fileContent = await _pdfRenderer.RenderComponent<DeclaracaoPedidoExcecaoPdf>(model.GetAsDictionary());

            return File(fileContent, fileContentType, fileName);
        }

        [HttpPost]
        public async Task<IActionResult> SetAnexoPed(IFormFile file, int idPedido, int tipoAnexo)
        {
            return SuccessMessages(await _pedidosExcecaoServicesServices.SetAnexoPed(file, idPedido, tipoAnexo));
        }

        [HttpDelete]
        public async Task<IActionResult> DelAnexoPed(int idPedido, int idAnexo, int tipoAnexo)
        {
            return SuccessMessages(await _pedidosExcecaoServicesServices.DelAnexoPed(idPedido, idAnexo, tipoAnexo));
        }

        [HttpPost]
        public async Task<IActionResult> SetPedEstado([FromBody] SetPedEstadoRequest requestObj)
        {
            return SuccessMessages(await _pedidosExcecaoServicesServices.SetPedEstado(requestObj));
        }

        #endregion


        #region Dropdowns

        [HttpGet]
        public async Task<IActionResult> GetAnosEscolares(string tipologia)
        {
            return Json(await _pedidosExcecaoServicesServices.GetAnoEscolares("EXE", User.GetAnoLetivoSIME(), tipologia));
        }

        [HttpGet]
        public async Task<IActionResult> GetDisciplinasAnoEscolar(string ano_escolar, string tipologia)
        {
            return Json(await _pedidosExcecaoServicesServices.GetDisciplinas("EXE", User.GetAnoLetivoSIME(), ano_escolar, tipologia));
        }

        [HttpGet]
        public async Task<IActionResult> GetDisciplinasPLNM(string tipologia)
        {
            return Json(await _pedidosExcecaoServicesServices.GetDisciplinasPLNM(tipologia));
        }

        #endregion
    }
}
