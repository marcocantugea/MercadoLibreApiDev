using DataEF.Models.Global;
using DataEF.Models.MLCatalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Configurations.MLCatalogs
{
    public class MLSitesTableConfiguration : IEntityTypeConfiguration<MLSites>
    {
        public void Configure(EntityTypeBuilder<MLSites> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DefaultCurrencyId).HasMaxLength(5);
            builder.Property(p => p.MLId).HasMaxLength(3).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(55);
        }

    }
}
