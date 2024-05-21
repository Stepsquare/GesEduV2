using GesEdu.Shared.ExceptionHandler.Exceptions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IHelpers;
using GesEdu.Shared.Interfaces.ISevices;
using GesEdu.Shared.WebserviceModels.Manuais;
using GesEdu.Shared.WebserviceModels.Noticias;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.ServiceLayer.Services
{
    public class HomepageServices : BaseServices, IHomepageServices
    {

        public HomepageServices(IUnitOfWork unitOfWork, IGenericRestRequests genericRestRequests) : base(unitOfWork, genericRestRequests)
        {
        }

        public async Task<GetNoticiasResponse?> GetNoticias(string? idServico = null)
        {
            var headers = new Dictionary<string, string>
            {
                { "show_image", "N" },
                { "estado", "A" }
            };

            if (!string.IsNullOrEmpty(idServico))
                headers.Add("Id_servico", idServico);

            return await _genericRestRequests.Get<GetNoticiasResponse>("noticias", "getNoticias", headers);
        }
    }
}
