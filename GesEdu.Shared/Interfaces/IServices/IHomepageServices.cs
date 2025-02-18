using GesEdu.Shared.WebserviceModels.Adm;
using GesEdu.Shared.WebserviceModels.Manuais;
using GesEdu.Shared.WebserviceModels.Noticias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.IServices
{
    public interface IHomepageServices
    {
        Task<GetNoticiasResponse?> GetNoticias(string? id = null);
        Task<List<GetDominiosResponseItem>?> GetDominios(string code);
    }
}
