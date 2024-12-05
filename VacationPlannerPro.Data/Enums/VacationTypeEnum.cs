using System.ComponentModel.DataAnnotations;

namespace VacationPlannerPro.Data.Enums
{
    public enum VacationTypeEnum
    {
        [Display(Name = "Paid Leave")]
        PaidLeave = 1,

        [Display(Name = "Sick Leave")]
        SickLeave = 2,

        [Display(Name = "Unpaid Leave")]
        UnpaidLeave = 3,

        Other = 4
    }
}
