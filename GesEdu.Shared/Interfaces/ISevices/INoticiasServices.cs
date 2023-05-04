using GesEdu.Shared.WebserviceModels.Manuais;
using GesEdu.Shared.WebserviceModels.Noticias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.ISevices
{
    public interface INoticiasServices
    {
        Task<GetNoticiasResponse?> GetNoticias(string? id = null);
    }
}
