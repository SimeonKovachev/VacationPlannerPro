using VacationPlannerPro.Business.DTOs;

namespace VacationPlannerPro.Business.Interfaces
{
    public interface IProfessionService
    {
        Task<IEnumerable<ProfessionDTO>> GetAllAsync();
    }
}
