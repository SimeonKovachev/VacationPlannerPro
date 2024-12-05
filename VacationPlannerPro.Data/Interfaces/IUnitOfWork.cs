using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces.Repositories;

namespace VacationPlannerPro.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Project> Projects { get; }
        IGenericRepository<Leader> Leaders { get; }
        IGenericRepository<Entities.Task> Tasks { get; }
        IGenericRepository<Team> Teams { get; }
        IGenericRepository<Vacation> Vacations { get; }
        IGenericRepository<Worker> Workers { get; }
        IGenericRepository<Profession> Professions { get; }
        Task<int> SaveChangesAsync();
    }
}
