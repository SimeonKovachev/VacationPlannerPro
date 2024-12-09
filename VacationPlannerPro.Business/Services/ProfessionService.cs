using AutoMapper;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace VacationPlannerPro.Business.Services
{
    public class ProfessionService : IProfessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfessionDTO>> GetAllAsync()
        {
            var professions = await _unitOfWork.Professions.GetAllAsync();
            var orderedProfessions = professions.OrderBy(p => p.Name);
            return _mapper.Map<IEnumerable<ProfessionDTO>>(orderedProfessions);
        }

        public async Task<ProfessionDTO?> GetByIdAsync(Guid id)
        {
            var profession = await _unitOfWork.Professions.GetByIdAsync(id);
            return profession == null ? null : _mapper.Map<ProfessionDTO>(profession);
        }

        public async Task CreateAsync(ProfessionDTO dto)
        {
            var profession = _mapper.Map<Profession>(dto);
            await _unitOfWork.Professions.AddAsync(profession);
        }

        public async Task UpdateAsync(ProfessionDTO dto)
        {
            var profession = await _unitOfWork.Professions.GetByIdAsync(dto.Id);
            if (profession == null)
                throw new KeyNotFoundException("Profession not found.");

            _mapper.Map(dto, profession);
            await _unitOfWork.Professions.UpdateAsync(profession);
        }

        public async Task DeleteAsync(Guid id)
        {
            var profession = await _unitOfWork.Professions.GetByIdAsync(id);
            if (profession == null)
                throw new KeyNotFoundException("Profession not found.");

            await _unitOfWork.Professions.DeleteAsync(profession);
        }

        public async Task<PaginatedListDTO<ProfessionDTO>> GetProfessionsAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var (professions, totalCount) = await _unitOfWork.Professions.GetPaginatedWithIncludeAsync<object>(
                 pageNumber,
                 pageSize,
                 p => p.Name.Contains(searchTerm ?? string.Empty)
             );

            var professionDtos = _mapper.Map<List<ProfessionDTO>>(professions);

            return new PaginatedListDTO<ProfessionDTO>
            {
                Items = professionDtos,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
