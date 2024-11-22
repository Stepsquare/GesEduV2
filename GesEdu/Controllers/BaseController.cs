using Microsoft.AspNetCore.Mvc;

namespace GesEdu.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult SuccessMessage(string? message)
        {
            return Ok(new 
            {
                isMessage = true,
                message
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
