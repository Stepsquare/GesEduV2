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
    public class WebServiceErrorRepository : GenericRepository<WebServiceError>, IWebServiceErrorRepository
    {
        public WebServiceErrorRepository(GesEduDbContext context) : base(context)
        {
        }
    }
}
