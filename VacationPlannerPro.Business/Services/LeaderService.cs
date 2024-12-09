using AutoMapper;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace VacationPlannerPro.Business.Services
{
    public class LeaderService : ILeaderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeaderDTO>> GetAllAsync()
        {
            var leaders = await _unitOfWork.Leaders.GetLeadersWithProfessionsAsync();
            return _mapper.Map<IEnumerable<LeaderDTO>>(leaders);
        }

        public async Task<LeaderDTO?> GetByIdAsync(Guid id)
        {
            var leader = await _unitOfWork.Leaders.GetLeaderWithProfessionByIdAsync(id);
            return leader == null ? null : _mapper.Map<LeaderDTO>(leader);
        }

        public async Task CreateAsync(CreateLeaderDTO createLeaderDto)
        {
            var leader = _mapper.Map<Leader>(createLeaderDto);
            await _unitOfWork.Leaders.AddAsync(leader);
        }

        public async Task UpdateAsync(UpdateLeaderDTO updateLeaderDto)
        {
            var leader = await _unitOfWork.Leaders.GetByIdAsync(updateLeaderDto.Id);
            if (leader == null)
                throw new KeyNotFoundException("Leader not found.");

            _mapper.Map(updateLeaderDto, leader);
            await _unitOfWork.Leaders.UpdateAsync(leader);
        }

        public async Task DeleteAsync(Guid id)
        {
            var leader = await _unitOfWork.Leaders.GetByIdAsync(id);
            if (leader == null)
                throw new KeyNotFoundException("Leader not found.");

            await _unitOfWork.Leaders.DeleteAsync(leader);
        }

        public async Task<PaginatedListDTO<LeaderDTO>> GetLeadersAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var (leaders, totalCount) = await _unitOfWork.Leaders.GetPaginatedAsync(
               pageNumber,
               pageSize,
               l => string.IsNullOrEmpty(searchTerm) || l.FullName.Contains(searchTerm)
           );

            var leaderDtos = _mapper.Map<List<LeaderDTO>>(leaders);

            return new PaginatedListDTO<LeaderDTO>
            {
                Items = leaderDtos,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
