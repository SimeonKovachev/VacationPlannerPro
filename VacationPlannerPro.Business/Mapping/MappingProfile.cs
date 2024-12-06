using AutoMapper;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using VacationPlannerPro.Business.DTOs.ProjectDTOs;
using VacationPlannerPro.Business.DTOs.VacationDTOs;
using VacationPlannerPro.Business.DTOs.WorkerDTOs;
using VacationPlannerPro.Data.Entities;

namespace VacationPlannerPro.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Leader, LeaderDTO>().ReverseMap();
            CreateMap<CreateLeaderDTO, Leader>();
            CreateMap<UpdateLeaderDTO, Leader>();

            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<CreateProjectDTO, Project>();
            CreateMap<UpdateProjectDTO, Project>();

            CreateMap<Vacation, VacationDTO>()
                .ForMember(dest => dest.WorkerName, opt => opt.MapFrom(src => src.Worker.FullName));
            CreateMap<CreateVacationDTO, Vacation>();
            CreateMap<UpdateVacationDTO, Vacation>();

            CreateMap<Worker, WorkerDTO>()
               .ForMember(dest => dest.ProfessionName, opt => opt.MapFrom(src => src.Profession.Name));

            CreateMap<CreateWorkerDTO, Worker>();
            CreateMap<UpdateWorkerDTO, Worker>();

            CreateMap<Profession, ProfessionDTO>();
        }
    }
}
