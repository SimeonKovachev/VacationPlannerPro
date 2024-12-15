using VacationPlannerPro.Business.DTOs.TeamDTOs;
using VacationPlannerPro.Business.DTOs;

namespace VacationPlannerPro.Business.Interfaces
{
    public interface ITeamService
    {
        Task<PaginatedListDTO<TeamDTO>> GetTeamsAsync(int pageNumber, int pageSize, string? searchTerm = null);

        Task<TeamDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateTeamDTO dto);

        Task UpdateAsync(UpdateTeamDTO dto);

        Task DeleteAsync(Guid id);

        Task<TeamWithWorkersDTO?> GetTeamWithWorkersByIdAsync(Guid id);
    }

}
