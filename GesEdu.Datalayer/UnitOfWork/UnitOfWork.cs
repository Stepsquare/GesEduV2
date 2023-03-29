using GesEdu.Datalayer.Context;
using GesEdu.Datalayer.Repositories;
using GesEdu.Shared.Interfaces.IConfiguration;
using GesEdu.Shared.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Datalayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GesEduDbContext _dbContext;

        public ISigefeRequestLogRepository SigefeRequestLogs { get; private set; }

        public UnitOfWork(GesEduDbContext dbContext)
        {
            _dbContext = dbContext;
            SigefeRequestLogs = new SigefeRequestLogRepository(dbContext);
        }

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
