﻿using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.Manuais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.IServices
{
    public interface ILoginServices
    {
        Task<(List<Claim> claims, bool chooseUo, bool changePassword)> SignIn(string username, string password);
        Task<List<GenericPostResponse.Message>?> PasswordRecovery(string email);
        Task<List<GenericPostResponse.Message>?> PasswordChange(string username, string oldPassword, string newPassword);
        Task<List<GetUoResponseItem>?> GetUo();
        ClaimsPrincipal SetUo(GetUoResponseItem model, ClaimsPrincipal principal);
    }
}
