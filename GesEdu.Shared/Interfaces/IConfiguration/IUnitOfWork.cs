using GesEdu.Shared.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Shared.Interfaces.IConfiguration
{
    public interface IUnitOfWork : IDisposable
    {
        IWebServiceErrorRepository WebServiceErrors { get; }
        Task<int> SaveChangesAsync();
    }
}
