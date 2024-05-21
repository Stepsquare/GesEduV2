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
        Task<(bool IsAdmin, bool ChangePassword)> SignIn(string username, string password);
        Task SignOut();
        Task<string?> PasswordRecovery(string email);
        Task<string?> PasswordChange(string username, string oldPassword, string newPassword);
        Task<List<GetUoResponseItem>?> GetUo(string? idServico);
        Task SetUo(GetUoResponseItem model, ClaimsPrincipal principal);
    }
}
