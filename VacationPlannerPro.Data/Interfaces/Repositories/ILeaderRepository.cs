using VacationPlannerPro.Data.Entities;

namespace VacationPlannerPro.Data.Interfaces.Repositories
{
    public interface ILeaderRepository : IGenericRepository<Leader>
    {
        Task<IEnumerable<Leader>> GetLeadersWithProfessionsAsync();

        Task<Leader?> GetLeaderWithProfessionByIdAsync(Guid id);
    }
}
