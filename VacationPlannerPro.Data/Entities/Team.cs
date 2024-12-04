using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Data.Entities
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50, ErrorMessage = "Team Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        public Guid ProjectId { get; set; }
        public Guid LeaderId { get; set; }

        [Range(1, 100, ErrorMessage = "Number of Workers must be between 1 and 100.")]
        public int NumberOfWorkers { get; set; }

        public virtual Project Project { get; set; }
        public virtual Leader Leader { get; set; }
        public virtual ICollection<Worker> Workers { get; set; } = new HashSet<Worker>();
    }
}
