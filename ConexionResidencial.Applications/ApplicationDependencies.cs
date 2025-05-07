using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConexionResidencial.Applications.Interfaces;
using ConexionResidencial.Applications.Services;

namespace ConexionResidencial.Applications
{
    public static class ApplicationDependencies
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICondominiosService, CondominioService>();
        }
    }
}
