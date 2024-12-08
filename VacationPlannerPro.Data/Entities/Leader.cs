using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Data.Entities
{
    public class Leader
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
        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
