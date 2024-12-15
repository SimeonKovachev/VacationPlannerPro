using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Business.DTOs.TeamDTOs
{
    public class UpdateTeamDTO : CreateTeamDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
