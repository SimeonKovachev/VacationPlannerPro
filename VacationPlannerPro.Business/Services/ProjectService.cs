using AutoMapper;
using VacationPlannerPro.Business.DTOs.ProjectDTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;

namespace VacationPlannerPro.Business.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllAsync()
        {
            var projects = await _unitOfWork.Projects.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO?> GetByIdAsync(Guid id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            return project == null ? null : _mapper.Map<ProjectDTO>(project);
        }

        public async Task CreateAsync(CreateProjectDTO createProjectDto)
        {
            var project = _mapper.Map<Project>(createProjectDto);
            await _unitOfWork.Projects.AddAsync(project);
        }

        public async Task UpdateAsync(UpdateProjectDTO updateProjectDto)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(updateProjectDto.Id);
            if (project == null)
                throw new KeyNotFoundException("Project not found.");

            _mapper.Map(updateProjectDto, project);
            await _unitOfWork.Projects.UpdateAsync(project);
        }

        public async Task DeleteAsync(Guid id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new KeyNotFoundException("Project not found.");

            await _unitOfWork.Projects.DeleteAsync(project);
        }
    }
}
