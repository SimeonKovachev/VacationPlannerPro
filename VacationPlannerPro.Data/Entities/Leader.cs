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

        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        public int Age { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Profession cannot exceed 50 characters.")]
        public string Profession { get; set; }

        // Navigation Properties
        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
