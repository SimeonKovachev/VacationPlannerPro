using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Data.Entities
{
    public class Worker
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        [Required]
        public Guid ProfessionId { get; set; }

        public virtual Profession Profession { get; set; }

        // Navigation Properties
        public virtual ICollection<TeamWorker> TeamWorkers { get; set; } = new HashSet<TeamWorker>();
        public virtual ICollection<Vacation> Vacations { get; set; } = new HashSet<Vacation>();
    }
}
