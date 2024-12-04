using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacationPlannerPro.Data.Interfaces;
using VacationPlannerPro.Data.Repositories;

namespace VacationPlannerPro.Business.Config
{
    public static class ServicesConfiguration
    {
        public static void AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
