﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VacationPlannerPro.Web.Helpers
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                       .Cast<TEnum>()
                       .Select(e => new SelectListItem
                       {
                           Value = Convert.ToInt32(e).ToString(),
                           Text = e.GetType()
                                   .GetMember(e.ToString())
                                   .First()
                                   .GetCustomAttribute<DisplayAttribute>()?.Name ?? e.ToString()
                       });
        }

        public static string GetEnumDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttribute<DisplayAttribute>();
            return attribute != null ? attribute.Name : value.ToString();
        }
    }
}
