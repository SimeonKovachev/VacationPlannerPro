using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using Task = System.Threading.Tasks.Task;

namespace VacationPlannerPro.Business.Interfaces
{
    public interface ILeaderService
    {
        Task<IEnumerable<LeaderDTO>> GetAllAsync();

        Task<LeaderDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateLeaderDTO leader);

        Task UpdateAsync(UpdateLeaderDTO leader);

        Task DeleteAsync(Guid id);
    }
}
