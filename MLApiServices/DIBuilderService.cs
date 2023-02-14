using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MLApiConnector.Contractors;
using MLApiConnector.Resources;
using MLApiContractors;

namespace MLApiServices
{
    public class DIBuilderService
    {
        public static void ConfigureDIServices(IServiceCollection services) {
            
            services.AddScoped<IGlobalConfigurationServices, GlobalConfigurationsServices>();
            services.AddScoped<IAuthResource,AuthResource>();
            services.AddScoped<ISites,Sites>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IUsers, Users>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
