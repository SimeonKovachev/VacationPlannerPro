using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VacationPlannerPro.Business.DTOs.WorkerDTOs;

namespace VacationPlannerPro.Business.DTOs.TeamDTOs
{
    public class TeamDTO
    {
        public Guid Id { get; set; }

        [DisplayName("Team Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Team Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; } 

        [Required]
        public Guid LeaderId { get; set; }

        [DisplayName("Leader Name")]
        public string LeaderName { get; set; }

        [DisplayName("Number of Workers")]
        public int NumberOfWorkers { get; set; }

        public List<WorkerDTO> Workers { get; set; } = new List<WorkerDTO>(); // Detailed worker information
    }
}
