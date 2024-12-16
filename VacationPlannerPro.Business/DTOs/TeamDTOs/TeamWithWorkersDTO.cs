using VacationPlannerPro.Business.DTOs.WorkerDTOs;

namespace VacationPlannerPro.Business.DTOs.TeamDTOs
{
    public class TeamWithWorkersDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ProjectName { get; set; }

        public string LeaderName { get; set; }

        public Guid ProjectId { get; set; }

        public Guid LeaderId { get; set; }

        public List<WorkerDTO> Workers { get; set; } = new List<WorkerDTO>();

        public int NumberOfWorkers => Workers?.Count ?? 0;
    }
}
