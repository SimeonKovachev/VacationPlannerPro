using VacationPlannerPro.Business.DTOs.ProjectDTOs;

namespace VacationPlannerPro.Business.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAllAsync();

        Task<ProjectDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateProjectDTO createProjectDto);

        Task UpdateAsync(UpdateProjectDTO updateProjectDto);

        Task DeleteAsync(Guid id);
    }
}
