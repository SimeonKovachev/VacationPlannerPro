using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces.Repositories;

namespace VacationPlannerPro.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Project> Projects { get; }
        IGenericRepository<Leader> Leaders { get; }
        ITeamRepository Teams { get; }
        IGenericRepository<Vacation> Vacations { get; }
        IWorkerRepository Workers { get; }
        IGenericRepository<Profession> Professions { get; }
        Task<int> SaveChangesAsync();
    }
}
