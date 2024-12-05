using AutoMapper;
using VacationPlannerPro.Business.DTOs;
using VacationPlannerPro.Business.DTOs.LeaderDTOs;
using VacationPlannerPro.Business.DTOs.ProjectDTOs;
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

            CreateMap<Profession, ProfessionDTO>();
        }
    }
}
