using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Business.DTOs.WorkerDTOs
{
    public class UpdateWorkerDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 65.")]
        public int Age { get; set; }

        [Required]
        public Guid ProfessionId { get; set; }
    }
}
