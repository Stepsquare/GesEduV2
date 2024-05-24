using GesEdu.Shared.ExceptionHandler.Exceptions;
using GesEdu.Shared.Interfaces.IConfiguration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;

namespace GesEdu.ServiceLayer.Services
{
    public abstract class BaseServices(IHttpClientFactory httpClientFactory, IUnitOfWork unitOfWork)
    {
        protected readonly HttpClient _client = httpClientFactory.CreateClient("sigefeClient");
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;

        protected async Task<T?> SendAsync<T>(HttpRequestMessage request) where T : class
        {
            using (var response = await _client.SendAsync(request))
            {
                var content = await response.Content.ReadAsStringAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;

                if (!response.IsSuccessStatusCode)
                    ErrorResponseHandler(content, contentType, response.StatusCode);

                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        private void ErrorResponseHandler(string? content, string? contentType, HttpStatusCode statusCode)
        {
            List<string> messages = new List<string>();

            if (!string.IsNullOrEmpty(content)
                && !string.IsNullOrEmpty(contentType) 
                && contentType == "application/json")
            {
                dynamic jsonObj = JObject.Parse(content);

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

            throw new WebserviceException(statusCode, messages.ToArray());
        }
    }
}
