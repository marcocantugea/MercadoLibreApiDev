using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MLApiContractors;

namespace MLApiServices
{
    public class DIBuilderService
    {
        public static void ConfigureDIServices(IServiceCollection services) {
            
            services.AddScoped<IGlobalConfigurationServices, GlobalConfigurationsServices>();

        }
    }
}
