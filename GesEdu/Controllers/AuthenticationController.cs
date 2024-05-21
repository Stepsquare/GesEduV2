using GesEdu.Models.AuthenticationViewModels;
using GesEdu.Shared.Interfaces.ISevices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GesEdu.Shared.Extensions;
using System.Security.Claims;
using GesEdu.Shared.WebserviceModels.Manuais;
using GesEdu.Models;

namespace GesEdu.Controllers
{
    [Authorize]
    [Route("/{action}")]
    public class AuthenticationController : Controller
    {
        private readonly ILoginServices _loginServices;

        public AuthenticationController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        #region Views
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult PasswordRecovery()
        {
            return View();
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult ChooseUo()
        {
            return View();
        }

        #endregion

        #region Requests

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (IsAdmin, ChangePassword) = await _loginServices.SignIn(model.Username!, model.Password!);

            if (ChangePassword)
                return Ok(new AjaxSuccessModel().AddRedirectUrl(Url.Action("PasswordChange", "Authentication")));

            if (IsAdmin)
                return Ok(new AjaxSuccessModel().AddRedirectUrl(Url.Action("ChooseUo", "Authentication")));

            return Ok(new AjaxSuccessModel().AddRedirectUrl(Url.Action("Index", "Home")));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _loginServices.SignOut();

            return RedirectToAction("Login", "Authentication");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PasswordRecovery(PasswordRecoveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return Ok(new AjaxSuccessModel().AddMessage(await _loginServices.PasswordRecovery(model.Email!)));
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _loginServices.PasswordChange(User.GetUsername(), model.OldPassword!, model.NewPassword!);

            if (User.IsInRole("ADMIN"))
                return Ok(new { isRedirect = true, url = Url.Action("ChooseUo", "Authentication") });

            return Ok(new AjaxSuccessModel().AddRedirectUrl(Url.Action("Index", "Home")));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<JsonResult> GetUo()
        {
            var result = await _loginServices.GetUo(User.GetIdServico());

            return Json(result?.Select(x => new {
                nome_text_field = $"{x.Cod_agrupamento} - {x.Nome}",
                x.Nome,
                x.Cod_agrupamento,
                x.Nif_servico
            }).ToArray());
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> SetUo([FromBody] GetUoResponseItem model)
        {
            await _loginServices.SetUo(model, HttpContext.User);

            return Ok(new AjaxSuccessModel().AddRedirectUrl(Url.Action("Index", "Home")));
        }

        #endregion
    }
}
