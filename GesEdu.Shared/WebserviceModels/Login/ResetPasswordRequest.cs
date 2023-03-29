using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.WebserviceModels.Login
{
    public class ResetPasswordRequest
    {
        public string? username { get; set; }
        public string? email { get; set; }
    }
}
