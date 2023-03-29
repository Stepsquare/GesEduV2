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
        ISigefeRequestLogRepository SigefeRequestLogs { get; }
        Task<int> SaveChangesAsync();
    }
}
