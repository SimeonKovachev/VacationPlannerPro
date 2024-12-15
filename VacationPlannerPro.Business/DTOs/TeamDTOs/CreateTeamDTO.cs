using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Business.DTOs.TeamDTOs
{
    public class CreateTeamDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Team Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        public Guid ProjectId { get; set; }

        public Guid LeaderId { get; set; }

        public List<Guid> WorkerIds { get; set; } = new();
    }
}
