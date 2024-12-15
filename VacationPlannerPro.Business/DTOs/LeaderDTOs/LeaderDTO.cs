﻿using System.ComponentModel;

namespace VacationPlannerPro.Business.DTOs.LeaderDTOs
{
    public class LeaderDTO
    {
        public Guid Id { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [DisplayName("Age")]
        public int Age { get; set; }

        [DisplayName("Profession")]
        public Guid ProfessionId { get; set; }

        [DisplayName("Profession Name")]
        public string ProfessionName { get; set; }
    }
}
