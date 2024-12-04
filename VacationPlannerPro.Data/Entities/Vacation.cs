using System.ComponentModel.DataAnnotations;
using VacationPlannerPro.Data.Enums;

namespace VacationPlannerPro.Data.Entities
{
    public class Vacation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public VacationTypeEnum Type { get; set; }

        public Guid WorkerId { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
