using VacationPlannerPro.Data.Entities;

namespace VacationPlannerPro.Data.Interfaces.Repositories
{
    public interface IWorkerRepository : IGenericRepository<Worker>
    {
        Task<IEnumerable<Worker>> GetWorkersWithProfessionsAsync();

        Task<Worker?> GetWorkerWithProfessionByIdAsync(Guid id);
    }
}
