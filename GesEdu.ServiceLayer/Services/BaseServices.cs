using GesEdu.Shared.DatabaseEntities;
using GesEdu.Shared.ExceptionHandler.Exceptions;
using GesEdu.Shared.Extensions;
using GesEdu.Shared.Interfaces.IConfiguration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace GesEdu.ServiceLayer.Services
{
    public abstract class BaseServices(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IUnitOfWork unitOfWork)
    {
        protected readonly HttpClient _client = httpClientFactory.CreateClient("sigefeClient");
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

        protected async Task<T?> SendAsync<T>(HttpRequestMessage request) where T : class
        {
            using (var response = await _client.SendAsync(request))
            {
                if (!response.IsSuccessStatusCode)
                    await ErrorResponseHandler(response, request);

                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        private async Task ErrorResponseHandler(HttpResponseMessage response, HttpRequestMessage request)
        {
            var error = new WebServiceError
            {
                User = _httpContext.User.GetUsername(),
                Url = request.RequestUri?.ToString(),
                HttpMethod = request.Method.ToString(),
                RequestHeaders = request.Headers?.ToString(),
                RequestContent = request.Method == HttpMethod.Post ? request.Content?.ReadAsStringAsync().Result : null,
                ResponseContent = response.Content.ReadAsStringAsync().Result,
                ResponseContentType = response.Content.Headers.ContentType?.MediaType,
                StatusCode = (int)response.StatusCode,
                StatusMessage = response.StatusCode.ToString(),
            };

            _unitOfWork.WebServiceErrors.Add(error);

            await _unitOfWork.SaveChangesAsync();

            List<string> messages = new List<string>();

            try
            {
                dynamic jsonObj = JObject.Parse(error.ResponseContent);

                foreach (dynamic message in jsonObj.messages)
                {
                    string msg = message.msg;
                    messages.Add(msg);
                }
            }
            catch (Exception)
            {
                messages.Add("Erro na chamada ao webservice.");
            }

            throw new WebserviceException((HttpStatusCode)error.StatusCode, messages.ToArray());
        }
    }
}