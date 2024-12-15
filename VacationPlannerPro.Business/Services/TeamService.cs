using VacationPlannerPro.Business.DTOs.TeamDTOs;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;
using AutoMapper;

namespace VacationPlannerPro.Business.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedListDTO<TeamDTO>> GetTeamsAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var (teams, totalCount) = await _unitOfWork.Teams.GetTeamsPaginatedAsync(pageNumber, pageSize, searchTerm);

            return new PaginatedListDTO<TeamDTO>
            {
                Items = _mapper.Map<List<TeamDTO>>(teams),
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }


        public async Task<TeamDTO?> GetByIdAsync(Guid id)
        {
            var team = await _unitOfWork.Teams.GetTeamByIdWithDetailsAsync(id);
            return team == null ? null : _mapper.Map<TeamDTO>(team);
        }

        public async Task CreateAsync(CreateTeamDTO dto)
        {
            var team = _mapper.Map<Team>(dto);

            foreach (var workerId in dto.WorkerIds)
            {
                team.TeamWorkers.Add(new TeamWorker
                {
                    TeamId = team.Id,
                    WorkerId = workerId
                });
            }

            await _unitOfWork.Teams.AddAsync(team);
        }

        public async Task UpdateAsync(UpdateTeamDTO dto)
        {
            var team = await _unitOfWork.Teams.GetTeamByIdWithDetailsAsync(dto.Id);
            if (team == null)
                throw new KeyNotFoundException("Team not found.");

            _mapper.Map(dto, team);

            var currentWorkerIds = team.TeamWorkers.Select(tw => tw.WorkerId).ToList();

            var workersToAdd = dto.WorkerIds.Except(currentWorkerIds).ToList();

            var workersToRemove = currentWorkerIds.Except(dto.WorkerIds).ToList();

            foreach (var workerId in workersToAdd)
            {
                team.TeamWorkers.Add(new TeamWorker
                {
                    TeamId = team.Id,
                    WorkerId = workerId
                });
            }

            foreach (var workerId in workersToRemove)
            {
                var teamWorker = team.TeamWorkers.FirstOrDefault(tw => tw.WorkerId == workerId);
                if (teamWorker != null)
                {
                    team.TeamWorkers.Remove(teamWorker);
                }
            }

            await _unitOfWork.Teams.UpdateAsync(team);
        }

        public async Task DeleteAsync(Guid id)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            if (team == null)
                throw new KeyNotFoundException("Team not found.");

            await _unitOfWork.Teams.DeleteAsync(team);
        }

        public async Task<TeamWithWorkersDTO?> GetTeamWithWorkersByIdAsync(Guid id)
        {
            var team = await _unitOfWork.Teams.GetTeamByIdWithDetailsAsync(id);
            return team == null ? null : _mapper.Map<TeamWithWorkersDTO>(team);
        }
    }
}
