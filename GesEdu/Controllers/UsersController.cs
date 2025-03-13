using GesEdu.Helpers.Breadcrumbs;
using GesEdu.Models;
using GesEdu.Models.AuthenticationViewModels;
using GesEdu.Models.UsersViewModels;
using GesEdu.ServiceLayer.Services;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IServices;
using GesEdu.Shared.SearchParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GesEdu.Controllers
{
    [Authorize(Policy = "UserManagementAccess")]
    [Route("/{controller}/{action}")]
    public class UsersController : BaseController
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        #region Views

        [Breadcrumb(Title = "Gerir utilizadores")]
        public async Task<IActionResult> Index()
        {
            var perfis = await _userServices.GetPerfis();

            var model = new NewUserViewModel(perfis!);

            return View(model);
        }

        public async Task<IActionResult> UserPermissionModal(int userId)
        {
            var userDetail = await _userServices.GetUtilizador(userId);
            var perfis = await _userServices.GetPerfis();

            var model = new UserPermissionViewModel(perfis!, userDetail!);

            return PartialView("_userPermissionModalPartial", model);
        }

        #endregion


        #region Requests
        [HttpPost]
        public async Task<IActionResult> Search(GetUtilizadoresParams searchParams)
        {
            var model = await _userServices.GetUtilizadores(searchParams);

            return PartialView("_userSearchResultPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitNewUser(NewUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var responseMessage = await _userServices.CriarUtilizador(model.Nome!, model.Email!, model.Password!, model.Perfis.ToDictionary(x => x.Id, x => x.IsChecked));

            return SuccessMessage(responseMessage);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleUser(int userId, bool isActive, bool isIgefeUser)
        {
            var responseMessage = await _userServices.AlterarEstadoUtilizador(userId, isActive, isIgefeUser);

            return SuccessMessage(responseMessage);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleUserProfile(int userId, int profileId, bool isActive, bool isIgefeUser)
        {
            var responseMessage = await _userServices.AlterarPerfilUtilizador(userId, profileId, isActive);

            return SuccessMessage(responseMessage);
        }

        #endregion
    }
}
