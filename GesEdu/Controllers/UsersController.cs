using GesEdu.Shared.Interfaces.ISevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GesEdu.Controllers
{
    [Authorize(Roles = "ADMIM, USER_MANAGER")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        #region Views
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Requests

        #endregion
    }
}
