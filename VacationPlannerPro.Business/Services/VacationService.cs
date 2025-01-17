﻿using AutoMapper;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.DTOs.VacationDTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Interfaces;

namespace VacationPlannerPro.Business.Services
{
    public class VacationService : IVacationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VacationDTO>> GetAllAsync()
        {
            var vacations = await _unitOfWork.Vacations.GetAllAsync();
            return _mapper.Map<IEnumerable<VacationDTO>>(vacations);
        }

        public async Task<VacationDTO?> GetByIdAsync(Guid id)
        {
            var vacation = await _unitOfWork.Vacations.GetEntityWithNavigationPropertyByIdAsync<Vacation, Worker>(id, v => v.Worker);
            return vacation == null ? null : _mapper.Map<VacationDTO>(vacation);
        }

        public async Task CreateAsync(CreateVacationDTO createVacationDto)
        {
            var vacation = _mapper.Map<Vacation>(createVacationDto);
            await _unitOfWork.Vacations.AddAsync(vacation);
        }

        public async Task UpdateAsync(UpdateVacationDTO updateVacationDto)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(updateVacationDto.Id);
            if (vacation == null)
                throw new KeyNotFoundException("Vacation not found.");

            _mapper.Map(updateVacationDto, vacation);
            await _unitOfWork.Vacations.UpdateAsync(vacation);
        }

        public async Task DeleteAsync(Guid id)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(id);
            if (vacation == null)
                throw new KeyNotFoundException("Vacation not found.");

            await _unitOfWork.Vacations.DeleteAsync(vacation);
        }

        public async Task<PaginatedListDTO<VacationDTO>> GetVacationsAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var (vacations, totalCount) = await _unitOfWork.Vacations.GetPaginatedWithIncludeAsync(
                 pageNumber,
                 pageSize,
                 v => string.IsNullOrEmpty(searchTerm) || v.Worker.FullName.Contains(searchTerm),
                 v => v.StartDate,
                 v => v.Worker
             );

            var vacationDtos = _mapper.Map<List<VacationDTO>>(vacations);

            return new PaginatedListDTO<VacationDTO>
            {
                Items = vacationDtos,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
