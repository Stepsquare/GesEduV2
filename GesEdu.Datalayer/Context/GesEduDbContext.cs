using GesEdu.Shared.DatabaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesEdu.Datalayer.Context
{
    public class GesEduDbContext : DbContext
    {
        public DbSet<SigefeRequestLog> SIGeFERequestLogs { get; set; }

        public GesEduDbContext(DbContextOptions<GesEduDbContext> options) : base(options)
        {
        }
    }
}
