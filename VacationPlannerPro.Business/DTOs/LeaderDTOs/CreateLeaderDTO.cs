using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Business.DTOs.LeaderDTOs
{
    public class CreateLeaderDTO
    {
        [Required]
        public string FullName { get; set; }

        [Range(18, 100)]
        public int Age { get; set; }

        [Required]
        public int ProfessionId { get; set; }
    }
}
