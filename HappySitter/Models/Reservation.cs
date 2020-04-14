using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HappySitter.Models
{

    public enum ReservationStatus
    {
        PaymentWaiting = 0
        ,Booked
        , Canceled
        , Done
    }

    public class Reservation
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string SitterId { get; set; }
        [Display(Name = "Sitter Name")]
        public string SitterUserName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ServiceDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        [Display(Name = "Total Cost(including Hst)")]
        public double Cost { get; set; } //including HSG
        public double PlatformFee { get; set; }
        [Display(Name = "Total Cost(excluding Hst)")]
        public double TotalCost { get; set; }
        public double CostPerHour { get; set; }
        public double Hst { get; set; }
        [Display(Name = "Status")]
        public ReservationStatus ReservationStatus { get; set; }
        [Display(Name = "Registered Date")]
        public DateTime RegistrationDateTime{ get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? CancelDateTime { get; set; }
    }
}