using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Business.Mapping;
using VacationPlannerPro.Business.Services;
using VacationPlannerPro.Data.Interfaces;
using VacationPlannerPro.Data.Interfaces.Repositories;
using VacationPlannerPro.Data.Repositories;

namespace VacationPlannerPro.Business.Config
{
    public static class ServicesConfiguration
    {
        public static void AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddScoped<ILeaderService, LeaderService>();
            services.AddScoped<IProfessionService, ProfessionService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IVacationService, VacationService>();
            services.AddScoped<IWorkerService, WorkerService>();

            //Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<ILeaderRepository, LeaderRepository>();

            //AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
