using GesEdu.Shared.DatabaseEntities;
using GesEdu.Shared.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Datalayer.Repositories
{
    public class SigefeRequestLogRepository : GenericRepository<SigefeRequestLog>, ISigefeRequestLogRepository
    {
        public SigefeRequestLogRepository(GesEduDbContext context) : base(context)
        {
        }
    }
}
