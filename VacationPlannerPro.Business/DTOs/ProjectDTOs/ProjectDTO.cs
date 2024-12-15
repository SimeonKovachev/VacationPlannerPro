using System.ComponentModel;

namespace VacationPlannerPro.Business.DTOs.ProjectDTOs
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }

        [DisplayName("Project Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}
