using System.ComponentModel;
using VacationPlannerPro.Data.Enums;

namespace VacationPlannerPro.Business.DTOs.VacationDTOs
{
    public class VacationDTO
    {
        public Guid Id { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Vacation Type")]
        public VacationTypeEnum Type { get; set; }

        [DisplayName("Vacation Type Name")]
        public string TypeName => Type.ToString();

        [DisplayName("Worker ID")]
        public Guid WorkerId { get; set; }

        [DisplayName("Worker Name")]
        public string WorkerName { get; set; }
    }
}
