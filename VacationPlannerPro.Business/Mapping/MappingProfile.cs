using AutoMapper;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using VacationPlannerPro.Business.DTOs.ProjectDTOs;
using VacationPlannerPro.Business.DTOs.TeamDTOs;
using VacationPlannerPro.Business.DTOs.VacationDTOs;
using VacationPlannerPro.Business.DTOs.WorkerDTOs;
using VacationPlannerPro.Data.Entities;

namespace VacationPlannerPro.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Leader, LeaderDTO>()
                .ForMember(dest => dest.ProfessionName, opt => opt.MapFrom(src => src.Profession.Name));
            CreateMap<CreateLeaderDTO, Leader>();
            CreateMap<UpdateLeaderDTO, Leader>();

            CreateMap<Project, ProjectDTO>();
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

            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader.FullName))
                .ForMember(dest => dest.NumberOfWorkers, opt => opt.MapFrom(src => src.TeamWorkers.Count));
            CreateMap<CreateTeamDTO, Team>();
            CreateMap<UpdateTeamDTO, Team>();

            CreateMap<Team, TeamWithWorkersDTO>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader.FullName))
                .ForMember(dest => dest.Workers, opt => opt.MapFrom(src => src.TeamWorkers.Select(tw => tw.Worker)))
                .ForMember(dest => dest.NumberOfWorkers, opt => opt.MapFrom(src => src.TeamWorkers.Count));

            CreateMap<Profession, ProfessionDTO>().ReverseMap();
        }
    }
}
