using GesEdu.Shared.WebserviceModels.Manuais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.ISevices
{
    public interface ILoginServices
    {
        Task<(bool IsLoginSuccessful, bool IsAdmin, bool ChangePassword, string? ErrorMessage)> SignIn(string username, string password);
        Task SignOut();
        Task<(bool IsSuccessful, string? ErrorMessage)> PasswordRecovery(string email);
        Task<(bool IsSuccessful, string? ErrorMessage)> PasswordChange(string username, string oldPassword, string newPassword);
        Task<List<GetUoResponse>?> GetUo(string? idServico);
        Task SetUo(GetUoResponse model, ClaimsPrincipal principal);
    }
}
