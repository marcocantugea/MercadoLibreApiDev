using DataEF.Models.MLUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Configurations.MLUsers
{
    public class MLUsersTableConfiguration : IEntityTypeConfiguration<MLUser>
    {
        
        public void Configure(EntityTypeBuilder<MLUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MLId).IsRequired();
            builder.Property(x=>x.NickName).IsRequired().HasMaxLength(32);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(24);
            builder.Property(x => x.Password).HasMaxLength(15);
        }
    }
}
