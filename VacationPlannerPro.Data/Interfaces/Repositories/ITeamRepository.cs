using VacationPlannerPro.Data.Entities;

namespace VacationPlannerPro.Data.Interfaces.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<(IEnumerable<Team> data, int totalCount)> GetTeamsPaginatedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null
        );

        Task<Team?> GetTeamByIdWithDetailsAsync(Guid id);

        Task<IEnumerable<Team>> GetAllWithProjectAndLeaderAsync();
    }
}
