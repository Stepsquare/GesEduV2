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

namespace GesEdu.Controllers
{
    [AllowAnonymous]
    [Route("/{action}")]
    public class AuthenticationController : Controller
    {
        private readonly ILoginServices _loginServices;

        public AuthenticationController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        #region Views
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult PasswordRecovery()
        {
            return View();
        }

        [Authorize]
        public IActionResult PasswordChange()
        {
            return View();
        }

        [Authorize(Roles = "ADMIM")]
        public IActionResult ChooseUo()
        {
            return View();
        }

        #endregion

        #region Requests
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var signInResult = await _loginServices.SignIn(model.Username, model.Password);

            if (!signInResult.IsLoginSuccessful)
            {
                ViewBag.ErrorMessage = signInResult.ErrorMessage;
                return View(model);
            }

            if (signInResult.ChangePassword)
                return RedirectToAction("PasswordChange", "Authentication");

            if (signInResult.IsAdmin)
                return RedirectToAction("ChooseUo", "Authentication");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _loginServices.SignOut();

            return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        public async Task<IActionResult> PasswordRecovery(PasswordRecoveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordRecoveryResult = await _loginServices.PasswordRecovery(model.Email);

                ViewBag.IsSuccessful = passwordRecoveryResult.IsSuccessful;
                ViewBag.ErrorMessage = passwordRecoveryResult.ErrorMessage;
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var passwordChangeResult = await _loginServices.PasswordChange(User.GetUsername(), model.OldPassword, model.NewPassword);

            ViewBag.IsSuccessful = passwordChangeResult.IsSuccessful;
            ViewBag.ErrorMessage = passwordChangeResult.ErrorMessage;

            if (User.IsInRole("ADMIN"))
                return RedirectToAction("ChooseUo", "Authentication");

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "ADMIM")]
        [HttpGet]
        public async Task<List<GetUoResponse>?> GetUo()
        {
            return await _loginServices.GetUo(User.GetIdServico());
        }

        [Authorize(Roles = "ADMIM")]
        [HttpPost]
        public async Task<IActionResult> SetUo([FromBody] GetUoResponse model)
        {
            try
            {
                await _loginServices.SetUo(model, HttpContext.User);

                return Ok(Url.Action("Index", "Home"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { messages = new string[] { ex.GetBaseException().Message } });
            }
        }

        #endregion
    }
}
