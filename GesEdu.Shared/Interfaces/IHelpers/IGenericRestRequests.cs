using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.IHelpers
{
    public interface IGenericRestRequests
    {
        Task<T?> Get<T>(string serviceModule, string service, IDictionary<string, string>? headers = null) where T : class;
        Task<TOutput?> Post<TOutput, TInput>(string serviceModule, string service, TInput bodyObject, IDictionary<string, string>? headers = null) where TOutput : class where TInput : class;
    }
}
