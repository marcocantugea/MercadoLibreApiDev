using DataEF.Configurations.Global;
using DataEF.Configurations.MLCatalogs;
using DataEF.Configurations.MLUsers;
using DataEF.Models.Global;
using DataEF.Models.MLCatalogs;
using DataEF.Models.MLUsers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Context
{
    public class MLApiDbContext: DbContext
    {
        public MLApiDbContext():base() {}

        public MLApiDbContext(DbContextOptions<MLApiDbContext> options) :base(options) {}

        public static MLApiDbContext GetMLApiDbContext(string sqlStringConnection)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MLApiDbContext>();
            optionsBuilder.UseSqlServer(sqlStringConnection);
            return new MLApiDbContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GlobalConfigurationTableConfig());
            modelBuilder.ApplyConfiguration(new MLSitesTableConfiguration());
            modelBuilder.ApplyConfiguration(new MLUsersTableConfiguration());
        }

        public DbSet<GlobalConfiguration> GlobalConfigurations { get; set; }
        public DbSet<MLSites> MLSites { get; set; }
        public DbSet<MLUser> MLUsers { get; set; }
    }
}
