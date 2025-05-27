using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams;
using GesEdu.Shared.WebserviceModels;
using GesEdu.Shared.WebserviceModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.IServices
{
    public interface IUserServices
    {
        Task<GetUtilizadorResponse?> GetUtilizador(int userId);
        Task<PaginatedResult<GetUtilizadoresResponseItem>> GetUtilizadores(GetUtilizadoresParams searchParams);
        Task<List<GetPerfisAppResponseItem>?> GetPerfis();
        Task<List<GenericPostResponse.Message>?> CriarUtilizador(string nome, string email, string password, IDictionary<int, bool> profiles);
        Task<List<GenericPostResponse.Message>?> AlterarPerfilUtilizador(int userId, int profileId, bool isChecked);
        Task<List<GenericPostResponse.Message>?> AlterarEstadoUtilizador(int userId, bool isActive, bool isIgefeUser);
    }
}
