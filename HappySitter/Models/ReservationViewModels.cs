using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HappySitter.Models
{
    public class ReservationViewModels
    {
        
    }

    public class GoogleMapMarker
    {
        public string Id { get; set; }
        public String UserName { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public String PhoneNumber { get; set; }
        public Double CostPerHour { get; set; }
        public Double TotalServiceHours { get; set; }
        public Double CostForServiceHours { get; set; }
        public Double PlatformFee { get; set; }
        public Double TotalCost { get; set; }
        public Double Hst { get; set; }
        public Double RateScore { get; set; }
        public String Contents { get; set; }
    }

    public class SearchSitterViewModel
    {
        public ApplicationUser User { get; set; }
        public List<GoogleMapMarker> SitterListMarker { get; set; }
        public DateTime ServiceDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string SitterListJsonSerialized { get; set; }
        public string JsonSerializeObject(Object obj)
        {
            string output = JsonConvert.SerializeObject(obj);
            return output;
        }

        public void SetJsonSerializedSitterList()
        {
            SitterListJsonSerialized = JsonConvert.SerializeObject(SitterListMarker);
        }
    }




}