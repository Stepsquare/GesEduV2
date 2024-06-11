using GesEdu.Datalayer.Repositories;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Datalayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GesEduDbContext _dbContext;

        public UnitOfWork(GesEduDbContext dbContext,
            IWebServiceErrorRepository webServiceErrors)
        {
            _dbContext = dbContext;
            WebServiceErrors = webServiceErrors;
        }

        public IWebServiceErrorRepository WebServiceErrors { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
