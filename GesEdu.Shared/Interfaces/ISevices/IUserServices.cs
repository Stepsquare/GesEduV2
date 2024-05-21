using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.ISevices
{
    public interface IUserServices
    {
        Task<GetUtilizadorResponse?> GetUtilizador(int userId);
        Task<PaginatedResult<GetUtilizadoresResponseItem>> GetUtilizadores(GetUtilizadoresParams searchParams);
        Task<List<GetPerfisAppResponseItem>?> GetPerfis();
        Task<string?> CriarUtilizador(string nome, string email, string password, IDictionary<int, bool> profiles);
        Task<string?> AlterarPerfilUtilizador(int userId, int profileId, bool isChecked);
        Task<string?> AlterarEstadoUtilizador(int userId, bool isActive);
    }
}
