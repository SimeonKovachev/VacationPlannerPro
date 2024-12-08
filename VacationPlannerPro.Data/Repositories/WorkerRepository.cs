using Microsoft.EntityFrameworkCore;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces.Repositories;

namespace VacationPlannerPro.Data.Repositories
{
    public class WorkerRepository : GenericRepository<Worker>, IWorkerRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Worker>> GetWorkersWithProfessionsAsync()
        {
            return await _context.Workers
                                 .Include(w => w.Profession)
                                 .ToListAsync();
        }

        public async Task<Worker?> GetWorkerWithProfessionByIdAsync(Guid id)
        {
            return await _context.Workers
                                 .Include(w => w.Profession)
                                 .FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}
