using GesEdu.Models.AuthenticationViewModels;
using GesEdu.Shared.Interfaces.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.WebserviceModels.Manuais;
using GesEdu.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace GesEdu.Controllers
{
    [Authorize]
    [Route("/{action}")]
    public class AuthenticationController : BaseController
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

        [Authorize(Policy = "ChooseUoPageAccess")]
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

            var (claims, chooseUo, changePassword) = await _loginServices.SignIn(model.Username!, model.Password!);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (changePassword)
                return SuccessRedirect(Url.Action("PasswordChange", "Authentication"));

            if (chooseUo)
                return SuccessRedirect(Url.Action("ChooseUo", "Authentication"));

            return SuccessRedirect(Url.Action("Index", "Home"));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

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

            return SuccessMessages(await _loginServices.PasswordRecovery(model.Email!));
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _loginServices.PasswordChange(User.GetUsername(), model.OldPassword!, model.NewPassword!);

            if (User.IsAdmin() || User.IsSimeDgeUser())
                return SuccessRedirect(Url.Action("ChooseUo", "Authentication"));

            return SuccessRedirect(Url.Action("Index", "Home"));
        }

        [Authorize(Policy = "ChooseUoPageAccess")]
        [HttpGet]
        public async Task<JsonResult> GetUo()
        {
            var result = await _loginServices.GetUo();

            return Json(result);
        }

        [Authorize(Policy = "ChooseUoPageAccess")]
        [HttpPost]
        public async Task<IActionResult> SetUo([FromBody] GetUoResponseItem model)
        {
            var newClaimsPrincipal = _loginServices.SetUo(model, HttpContext.User);

            await HttpContext.SignOutAsync();

            await HttpContext.SignInAsync(newClaimsPrincipal);

            return SuccessRedirect(Url.Action("Index", "Home"));
        }

        #endregion
    }
}
