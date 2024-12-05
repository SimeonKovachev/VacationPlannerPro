using VacationPlannerPro.Data.Enums;

namespace VacationPlannerPro.Business.DTOs.VacationDTOs
{
    public class VacationDTO
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public VacationTypeEnum Type { get; set; }

        public string TypeName => Type.ToString();

        public Guid WorkerId { get; set; }

        public string WorkerName { get; set; }
    }
}
