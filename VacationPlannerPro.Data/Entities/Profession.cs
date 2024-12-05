using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Data.Entities
{
    public class Profession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Profession name cannot exceed 50 characters.")]
        public string Name { get; set; }
    }
}
