using GesEdu.Shared.Pagination;
using GesEdu.Shared.SearchParams.SIME;
using GesEdu.Shared.WebserviceModels.SIME;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface IApreciacaoManuaisServices
    {
        Task<List<GetCiclosUOResponseItem>?> GetCiclos(string ano_letivo);
        Task<List<GetAnosEscolaresResponseItem>?> GetAnoEscolares(string ano_letivo);
        Task<List<GetDisciplinasAnoEscResponseItem>?> GetDisciplinas(string ano_letivo, string ano_escolar, string tipologia);
        Task<PaginatedResult<GetManuaisApreciadosResponseItem>> GetManuaisApreciados(GetManuaisApreciadosParams filter);
        Task<GetManuaisAprDetResponse?> GetManualApreciado(string id_manual, string id_ano_letivo);
        Task<GetManuaisSIMEResponse?> GetManuaisSIMEPdfExport(string ano_letivo, string ciclo);
        Task<string?> SetManualApreciado(SetManualAprDetRequest requestObj);
    }
}
