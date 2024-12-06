namespace VacationPlannerPro.Business.DTOs.WorkerDTOs
{
    public class WorkerDTO
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int ProfessionId { get; set; }

        public string ProfessionName { get; set; }
    }
}
