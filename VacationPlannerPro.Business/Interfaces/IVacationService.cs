using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.DTOs.VacationDTOs;

namespace VacationPlannerPro.Business.Interfaces
{
    public interface IVacationService
    {
        Task<IEnumerable<VacationDTO>> GetAllAsync();

        Task<VacationDTO?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateVacationDTO createVacationDto);

        Task UpdateAsync(UpdateVacationDTO updateVacationDto);

        Task DeleteAsync(Guid id);

        Task<PaginatedListDTO<VacationDTO>> GetVacationsAsync(int pageNumber, int pageSize, string? searchTerm = null);
    }
}
