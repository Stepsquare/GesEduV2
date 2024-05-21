using GesEdu.Shared.DatabaseEntities;
using GesEdu.Shared.ExceptionHandler.Exceptions;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IHelpers;
using GesEdu.Shared.WebserviceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GesEdu.ServiceLayer.Helpers
{
    public class GenericRestRequests : IGenericRestRequests
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClaimsPrincipal _user;

        public GenericRestRequests(IConfiguration config, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _user = httpContextAccessor.HttpContext.User;
            _config = config;
            _unitOfWork = unitOfWork;
        }

        public async Task<T?> Get<T>(string serviceModule, string service, IDictionary<string, string>? headers = null) where T : class
        {
            var client = new RestClient(WebServiceUrl(serviceModule, service));

            var request = new RestRequest();

            AddCredentialHeaders(request);

            if (headers != null && headers.Any())
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);

            var response = await client.ExecuteGetAsync<T>(request);

            ValidateRestResponse(response);

            return response.Data;
        }

        public async Task<TOutput?> Post<TOutput, TInput>(string serviceModule, string service, TInput bodyObject, IDictionary<string, string>? headers = null) where TOutput : class where TInput : class
        {
            var client = new RestClient(WebServiceUrl(serviceModule, service));

            var request = new RestRequest();

            AddCredentialHeaders(request);

            if (headers != null && headers.Any())
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);

            request.AddJsonBody(bodyObject);

            var response = await client.ExecutePostAsync<TOutput>(request);

            ValidateRestResponse(response);

            return response.Data;
        }

        #region Private methods
        private void AddCredentialHeaders(in RestRequest request)
        {
            request.AddHeader("username", _config["SigefeWebservicesCredentials:Username"]);
            request.AddHeader("password", _config["SigefeWebservicesCredentials:Password"]);
        }

        private string WebServiceUrl(string module, string service)
        {
            return string.Concat(_config["SigefeWebservicesCredentials:BaseUrl"], _config[$"SigefeWebservices:{module}:{service}"]);
        }

        private void ValidateRestResponse<T>(RestResponse<T> restResponse) where T : class
        {
            if (!restResponse.IsSuccessful || restResponse.Data == null)
            {
                List<string> messages = new List<string>();

                if (restResponse.ContentType == "application/json" && !string.IsNullOrEmpty(restResponse.Content))
                {
                    dynamic jsonObj = JObject.Parse(restResponse.Content);

                    foreach (dynamic message in jsonObj.messages)
                    {
                        string msg = message.msg;
                        messages.Add(msg);
                    }
                }
                else
                {
                    messages.Add("Erro na chamada ao webservice. Contactar suporte.");
                }

                throw new WebserviceException(restResponse.StatusCode, messages.ToArray());
            }
        }
        #endregion
    }
}
