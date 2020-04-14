using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using HappySitter.CustomExceptions;
using HappySitter.Utils;
using HappySitter.Models;

namespace HappySitter.DAL
{
    public class SqlService
    {
        private static ApplicationDbContext _db = new ApplicationDbContext();

        public static List<GoogleMapMarker> GetAvailableSitters(SearchSitterViewModel model)
        {

            DateTime date = model.ServiceDate;
            TimeSpan fromTime = model.FromTime;
            TimeSpan toTime = model.ToTime;
            double spendingHours = Math.Truncate(toTime.Subtract(fromTime).TotalHours * 100) / 100;

            DAL dal = new DAL(GetConnString("DefaultConnection"));
            string sqlText = @"
               select * from [dbo].[AspNetUsers] 
                   where Id in(
                  SELECT UserId from [dbo].[Schedules]
	                WHERE DayOfWeek = DATEPART(dw,@Date)
		                AND FromTime <= CAST(@FromTime AS Time)
		                AND ToTime >= CAST(@ToTime AS Time)
		                )
                        AND AccountActiveStatus = @AccountActiveStatus
                ";
            ArrayList sqlParams = new ArrayList
            {
                new SqlParam("@Date", date),
                new SqlParam("@FromTime", fromTime),
                new SqlParam("@ToTime", toTime),
                new SqlParam("@AccountActiveStatus", HappySitter.Models.AccountActiveStatus.IsActivated)
            };



            DataTable dt = dal.ExecuteSelectCommand(sqlText, sqlParams).Tables[0]; ;

            List<GoogleMapMarker> sitterList = new List<GoogleMapMarker>();

            foreach (DataRow r in dt.Rows)
            {
                double costPerHour = Convert.ToDouble(r["CostPerHour"].ToString());
                double costForServiceHours = Math.Truncate(costPerHour * spendingHours * 100) / 100;

                double platformFeePercentage = Convert.ToDouble(r["PlatformFeePercentage"].ToString());
                double platformFee = costForServiceHours * platformFeePercentage / 100;
                platformFee = Math.Truncate(platformFee * 100) / 100;
                double totalCost = costForServiceHours + platformFee;
                double hst = Math.Truncate(totalCost * 0.13 * 100) / 100;

                //temp for rate score
                Random random = new Random();
                int rateScore = random.Next(5, 10);

                sitterList.Add(
                    new GoogleMapMarker()
                    {
                        Id = r["Id"].ToString(),
                        UserName = r["UserName"].ToString(),
                        Latitude = Convert.ToDouble(r["Latitude"].ToString()),
                        Longitude = Convert.ToDouble(r["Longitude"].ToString()),
                        PhoneNumber = r["PhoneNumber"].ToString(),
                        CostPerHour = costPerHour,
                        TotalServiceHours = spendingHours,
                        CostForServiceHours = costForServiceHours,
                        PlatformFee = platformFee,
                        TotalCost = totalCost,
                        Hst = hst,
                        RateScore = rateScore
                    });
            }

            return sitterList;
        }

        public static string GetUserInfoById(string id, string returnField)
        {
            ApplicationUser user = _db.Users.Find(id);

            if (returnField == "UserName")
            {
                return user.UserName;
            }else if (returnField == "Address")
            {
                return user.StreetAddress + " " + user.City;
            }else if (returnField == "PhoneNumber")
            {
                return user.PhoneNumber;
            }

            return "";
        }

        public static string GetConnString(string name)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            connectionString = connectionString.Replace("$USERNAME", ConfigurationManager.AppSettings["EncryptedDBUsername"]);
            connectionString = connectionString.Replace("$PASSWORD", ConfigurationManager.AppSettings["EncryptedDBPassword"]);
            return connectionString;
        }
    }
}