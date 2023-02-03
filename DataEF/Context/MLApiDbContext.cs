using DataEF.Configurations.Global;
using DataEF.Models.Global;
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
        }

        public DbSet<GlobalConfiguration> GlobalConfigurations { get; set; }
    }
}
