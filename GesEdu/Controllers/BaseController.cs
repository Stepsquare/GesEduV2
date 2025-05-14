using GesEdu.Shared.WebserviceModels;
using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult SuccessMessages(List<GenericPostResponse.Message>? messages)
        {
            return Ok(new
            {
                isMessage = true,
                messages = messages?.Select(x => x.msg).ToArray() ?? []
            });
        }

        public IActionResult SuccessRedirect(string? url)
        {
            return Ok(new
            {
                isRedirect = true,
                url
            });
        }
    }
}
