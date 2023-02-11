using DataEF.Models.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Configurations.Global
{
    public class GlobalConfigurationTableConfig : IEntityTypeConfiguration<GlobalConfiguration>
    {
        public void Configure(EntityTypeBuilder<GlobalConfiguration> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(32).IsRequired();
            builder.Property(x => x.created).IsRequired();
            builder.HasData(GetData());
        }

        protected IEnumerable<GlobalConfiguration> GetData()
        {
            List<GlobalConfiguration> data = new List<GlobalConfiguration>()
            {
                new GlobalConfiguration()
                {
                    Id = 1,
                    Name= "CLIENT_ID",
                    active= true,
                    created= DateTime.Now,
                    Description="Mercado libre client id",
                    Value=""
                },
                new GlobalConfiguration()
                {
                    Id = 2,
                    Name= "CLIENT_SECRET",
                    active= true,
                    created= DateTime.Now,
                    Description="Mercado libre client secret",
                    Value=""
                },
                new GlobalConfiguration()
                {
                    Id = 3,
                    Name= "ML_CODE",
                    active= true,
                    created= DateTime.Now,
                    Description="Mercado libre code generated",
                    Value=""
                },
                new GlobalConfiguration()
                {
                    Id = 4,
                    Name= "ACCESS_TOKEN",
                    active= true,
                    created= DateTime.Now,
                    Description="Mercado libre token generated",
                    Value=""
                },
                new GlobalConfiguration()
                {
                    Id = 5,
                    Name= "ACCESS_TOKEN_EXPIRE",
                    active= true,
                    created= DateTime.Now,
                    Description="Mercado libre token exprire time miliseconds",
                    Value=""
                },
                new GlobalConfiguration()
                {
                    Id = 6,
                    Name= "ACCESS_TOKEN_USERID",
                    active= true,
                    created= DateTime.Now,
                    Description="Mercado libre token user id",
                    Value=""
                },
                new GlobalConfiguration()
                {
                    Id = 7,
                    Name= "REFRESH_TOKEN",
                    active= true,
                    created= DateTime.Now,
                    Description="Mercado libre refresh token",
                    Value=""
                },
                new GlobalConfiguration()
                {
                    Id = 8,
                    Name= "ACCESS_TOKEN_EXPIRE_DATE",
                    active= true,
                    created= DateTime.Now,
                    Description="Mercado libre exprire date",
                    Value=""
                },
            };

            return data;
        }
    }
}
