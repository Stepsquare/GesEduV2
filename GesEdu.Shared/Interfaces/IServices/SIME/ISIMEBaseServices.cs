using GesEdu.Shared.WebserviceModels.Adm;
using GesEdu.Shared.WebserviceModels.SIME;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.IServices.SIME
{
    public interface ISIMEBaseServices
    {
        Task<List<GetCiclosUOResponseItem>?> GetCiclos();
        Task<List<GetEscolasResponse.Escola>?> GetEscolas();

    }
}
