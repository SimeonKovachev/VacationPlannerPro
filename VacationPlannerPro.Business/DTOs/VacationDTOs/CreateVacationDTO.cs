using System.ComponentModel.DataAnnotations;
using VacationPlannerPro.Data.Enums;

namespace VacationPlannerPro.Business.DTOs.VacationDTOs
{
    public class CreateVacationDTO
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public VacationTypeEnum Type { get; set; }

        [Required]
        public Guid WorkerId { get; set; }
    }
}
