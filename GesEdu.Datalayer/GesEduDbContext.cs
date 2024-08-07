﻿using GesEdu.Shared.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace GesEdu.DataLayer
{
    public class GesEduDbContext : DbContext
    {
        public GesEduDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WebServiceError> WebServiceErrors { get; set; }
    }
}
