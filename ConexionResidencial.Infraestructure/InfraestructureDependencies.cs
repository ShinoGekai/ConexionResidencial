using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConexionResidencial.Core.Repositories;
using ConexionResidencial.Infraestructure.DataBase;
using ConexionResidencial.Infraestructure.Repository;

namespace ConexionResidencial.Infraestructure
{
    public static class InfrastructureDependencies
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DB_Context>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Conexion")));
            services.AddTransient<ICondominiosRepository, CondominiosRepository>();
        }
    }
}
