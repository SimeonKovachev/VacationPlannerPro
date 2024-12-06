using VacationPlannerPro.Business.DTOs.WorkerDTOs;

namespace VacationPlannerPro.Business.Interfaces
{
    public interface IWorkerService
    {
        Task<IEnumerable<WorkerDTO>> GetAllAsync();

        Task<WorkerDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateWorkerDTO createWorkerDto);

        Task UpdateAsync(UpdateWorkerDTO updateWorkerDto);

        Task DeleteAsync(Guid id);
    }
}
