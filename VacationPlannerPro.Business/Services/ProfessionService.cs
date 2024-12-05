using AutoMapper;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.Interfaces;
using VacationPlannerPro.Data.Interfaces;

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
    }
}
