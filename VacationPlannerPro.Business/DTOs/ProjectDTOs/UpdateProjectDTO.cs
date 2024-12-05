using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Business.DTOs.ProjectDTOs
{
    public class UpdateProjectDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Project Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
