using GesEdu.Controllers;
using GesEdu.Helpers.Breadcrumbs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Areas.SIME.Controllers
{
    [Area("SIME")]
    [Authorize(Policy = "SimeAccess")]
    public class PedidosExcecaoController : BaseController
    {
        [Breadcrumb(Title = "Pedidos de exceção - Listagem")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
