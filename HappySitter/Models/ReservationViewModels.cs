using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HappySitter.Models
{
    public class ReservationViewModels
    {
        
    }

    public class SearchSitterViewModel
    {
        public Reservation Reservation { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }




}