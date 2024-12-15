using Microsoft.EntityFrameworkCore; 
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces.Repositories;

namespace VacationPlannerPro.Data.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Team> data, int totalCount)> GetTeamsPaginatedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null)
        {
            var query = _context.Teams
                .Include(t => t.Project)
                .Include(t => t.Leader)
                .Include(t => t.TeamWorkers)
                    .ThenInclude(tw => tw.Worker)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.Name.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<Team?> GetTeamByIdWithDetailsAsync(Guid id)
        {
            return await _context.Teams
                .Include(t => t.Project)
                .Include(t => t.Leader)
                .Include(t => t.TeamWorkers)
                   .ThenInclude(tw => tw.Worker)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Team>> GetAllWithProjectAndLeaderAsync()
        {
            return await _context.Teams
                .Include(t => t.Project)
                .Include(t => t.Leader)
                .ToListAsync();
        }
    }
}
