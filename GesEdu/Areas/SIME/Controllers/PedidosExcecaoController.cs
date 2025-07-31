using Azure;
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
    public class PedidosExcecaoController : BaseController
    {

        private readonly IPedidosExcecaoServices _pedidosExcecaoServicesServices;
        private readonly PdfRenderer _pdfRenderer;

        public PedidosExcecaoController(IPedidosExcecaoServices pedidosExcecaoServicesServices, PdfRenderer pdfRenderer)
        {
            _pedidosExcecaoServicesServices = pedidosExcecaoServicesServices;
            _pdfRenderer = pdfRenderer;
        }

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

        [Breadcrumb(ResetAreaNodes = false)]
        public IActionResult Detalhe(string idPedido, string anoLetivo)
        {
            ViewData["BreadcrumbTitle"] = $"Pedido #{idPedido}";
            
            return View("Detalhe/Index");
        }

        [HttpPost]
        public async Task<IActionResult> SetPedExcecao([FromBody] SetPedExcecaoRequest requestObj, [FromHeader] bool goDetail)
        {
            var response = await _pedidosExcecaoServicesServices.SetPedExcecao(requestObj);

            if (goDetail)
                return RedirectToAction("Detalhe", "PedidosExcecao", new
                {
                    idPedido = response?.id_pedido_cab,
                    anoLetivo = User.GetAnoLetivoSIME()
                });

            return SuccessMessages(response?.messages);
        }

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
    }
}
