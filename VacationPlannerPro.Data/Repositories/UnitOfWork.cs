using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces.Repositories;
using VacationPlannerPro.Data.Interfaces;

namespace VacationPlannerPro.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Projects = new GenericRepository<Project>(_context);
            Leaders = new LeaderRepository(_context);
            Tasks = new GenericRepository<Entities.Task>(_context);
            Teams = new GenericRepository<Team>(_context);
            Vacations = new GenericRepository<Vacation>(_context);
            Workers = new WorkerRepository(_context);
            Professions = new GenericRepository<Profession>(_context);
        }

        public IGenericRepository<Project> Projects { get; }
        public ILeaderRepository Leaders { get; }
        public IGenericRepository<Entities.Task> Tasks { get; }
        public IGenericRepository<Team> Teams { get; }
        public IGenericRepository<Vacation> Vacations { get; }
        public IWorkerRepository Workers { get; }
        public IGenericRepository<Profession> Professions { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
