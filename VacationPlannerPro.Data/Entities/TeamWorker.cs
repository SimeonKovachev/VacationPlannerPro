
namespace VacationPlannerPro.Data.Entities
{
    public class TeamWorker
    {
        public Guid TeamId { get; set; }
        public Guid WorkerId { get; set; }

        public Team Team { get; set; }
        public Worker Worker { get; set; }
    }
}
