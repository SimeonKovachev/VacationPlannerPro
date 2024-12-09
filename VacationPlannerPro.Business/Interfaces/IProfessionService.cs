using VacationPlannerPro.Business.DTOs;

namespace VacationPlannerPro.Business.Interfaces
{
    public interface IProfessionService
    {
        Task<PaginatedListDTO<ProfessionDTO>> GetProfessionsAsync(int pageNumber, int pageSize, string? searchTerm = null);

        Task<IEnumerable<ProfessionDTO>> GetAllAsync();

        Task<ProfessionDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(ProfessionDTO dto);

        Task UpdateAsync(ProfessionDTO dto);

        Task DeleteAsync(Guid id);
    }
}
