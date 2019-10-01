using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HappyOrSad.Models
{
    public enum TimeIntervalType
    {
        Day = 1,
        Hour = 2,
        Minute = 3,
        Second = 4
    }
    public class TimeInterval
    {
        [Key]
        public int TimeIntervalID { get; set; }
        [Display(Name = "Time Interval Type")]
        public TimeIntervalType TimeIntervalType { get; set; }
        public int Value { get; set; }
    }
}