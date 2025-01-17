﻿using AutoMapper;
using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.DTOs.WorkerDTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;

namespace VacationPlannerPro.Business.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkerDTO>> GetAllAsync()
       {
            var workers = await _unitOfWork.Workers.GetWorkersWithProfessionsAsync();
            return _mapper.Map<IEnumerable<WorkerDTO>>(workers);
        }

        public async Task<WorkerDTO?> GetByIdAsync(Guid id)
        {
            var worker = await _unitOfWork.Workers.GetWorkerWithProfessionByIdAsync(id);
            return worker == null ? null : _mapper.Map<WorkerDTO>(worker);
        }

        public async Task CreateAsync(CreateWorkerDTO createWorkerDto)
        {
            var worker = _mapper.Map<Worker>(createWorkerDto);
            await _unitOfWork.Workers.AddAsync(worker);
        }

        public async Task UpdateAsync(UpdateWorkerDTO updateWorkerDto)
        {
            var worker = await _unitOfWork.Workers.GetByIdAsync(updateWorkerDto.Id);
            if (worker == null)
                throw new KeyNotFoundException("Worker not found.");

            _mapper.Map(updateWorkerDto, worker);
            await _unitOfWork.Workers.UpdateAsync(worker);
        }

        public async Task DeleteAsync(Guid id)
        {
            var worker = await _unitOfWork.Workers.GetByIdAsync(id);
            if (worker == null)
                throw new KeyNotFoundException("Worker not found.");

            await _unitOfWork.Workers.DeleteAsync(worker);
        }

        public async Task<PaginatedListDTO<WorkerDTO>> GetWorkersAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var (workers, totalCount) = await _unitOfWork.Workers.GetPaginatedWithIncludeAsync(
                 pageNumber,
                 pageSize,
                 l => string.IsNullOrEmpty(searchTerm) || l.FullName.Contains(searchTerm),
                 null,
                 l => l.Profession
             );

            var workerDtos = _mapper.Map<List<WorkerDTO>>(workers);

            return new PaginatedListDTO<WorkerDTO>
            {
                Items = workerDtos,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
