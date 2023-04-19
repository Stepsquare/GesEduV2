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
    public class NoticiasServices : INoticiasServices
    {
        private readonly HttpContext _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRestRequests _genericRestRequests;

        public NoticiasServices(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IGenericRestRequests genericRestRequests)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _unitOfWork = unitOfWork;
            _genericRestRequests = genericRestRequests;
        }

        public async Task<GetNoticiasResponse?> GetNoticias(string? idServico)
        {
            var headers = new Dictionary<string, string>();

            headers.Add("show_image", "N");
            headers.Add("estado", "A");

            if (!string.IsNullOrEmpty(idServico))
                headers.Add("Id_servico", idServico);

            return await _genericRestRequests.Get<GetNoticiasResponse>("noticias", "getNoticias", headers);
        }
    }
}
