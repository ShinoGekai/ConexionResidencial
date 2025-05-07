using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ConexionResidencial.App.Interfaces;
using ConexionResidencial.Services;

namespace ConexionResidencial.App.Services
{
    public static class PresentationServicesDependencies
    {
        public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            services.AddTransient<ICondominiosPresentationService, CondominiosPresentatioService>();
        }
    }
}
