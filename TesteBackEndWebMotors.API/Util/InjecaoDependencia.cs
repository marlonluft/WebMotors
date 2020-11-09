using Microsoft.Extensions.DependencyInjection;
using TesteBackEndWebMotors.Library.Repository;
using TesteBackEndWebMotors.Library.Services;

namespace TesteBackEndWebMotors.Util
{
    public static class InjecaoDependencia
    {
        public static void RegistrarInjecaoDependencia(this IServiceCollection services)
        {
            services.AddTransient<IDbContext, DbContext>();

            // Repository
            services.AddScoped<IAnuncioRepository, AnuncioRepository>();

            // Service
            services.AddScoped<IAnuncioService, AnuncioService>();
        }
    }
}
