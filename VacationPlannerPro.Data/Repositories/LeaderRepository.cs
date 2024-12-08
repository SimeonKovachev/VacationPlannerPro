using Microsoft.EntityFrameworkCore;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces.Repositories;

namespace VacationPlannerPro.Data.Repositories
{
    public class LeaderRepository : GenericRepository<Leader>, ILeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Leader>> GetLeadersWithProfessionsAsync()
        {
            return await _context.Leaders
                                 .Include(w => w.Profession)
                                 .ToListAsync();
        }

        public async Task<Leader?> GetLeaderWithProfessionByIdAsync(Guid id)
        {
            return await _context.Leaders
                                 .Include(w => w.Profession)
                                 .FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}
