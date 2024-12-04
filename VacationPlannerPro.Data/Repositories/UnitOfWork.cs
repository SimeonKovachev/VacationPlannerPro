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
            Tasks = new GenericRepository<Entities.Task>(_context);
            Teams = new GenericRepository<Team>(_context);
            Vacations = new GenericRepository<Vacation>(_context);
            Workers = new GenericRepository<Worker>(_context);
        }

        public IGenericRepository<Project> Projects { get; }
        public IGenericRepository<Entities.Task> Tasks { get; }
        public IGenericRepository<Team> Teams { get; }
        public IGenericRepository<Vacation> Vacations { get; }
        public IGenericRepository<Worker> Workers { get; }

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
