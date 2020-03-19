using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HappySitter.Models
{
    public enum DayOfWeek
    {
        Sunday = 1 //MSSQL sunday = 1 to  saturday = 7
        ,Monday
        ,Tuesday
        ,Wednesday
        ,Thursday
        ,Friday
        ,Saturday
    }

    public class DayIdWithName{
        public int DayId { get; set; }
        public string DayName { get; set; }
    }

    public class Schedule
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Day of Week")]
        [Required]
        [Range(1,7, ErrorMessage ="Please select the Day of Week.")]
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public DateTime DateLastModified { get; set; }


        public static List<DayIdWithName> GetAllDaysOfWeek()
        {
            List<DayIdWithName> allDaysOfWeek = new List<DayIdWithName>()
            {
                new DayIdWithName(){DayId = 1,DayName = "Sunday"},
                new DayIdWithName(){DayId = 2,DayName = "Monday"},
                new DayIdWithName(){DayId = 3,DayName = "Tuesday"},
                new DayIdWithName(){DayId = 4,DayName = "Wednesday"},
                new DayIdWithName(){DayId = 5,DayName = "Thursday"},
                new DayIdWithName(){DayId = 6,DayName = "Friday"},
                new DayIdWithName(){DayId = 7,DayName = "Saturday"}

            };

            return allDaysOfWeek;
        }
    }
}