using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappySitter.Utils
{
    public class AddressUtil
    {
        public static string BuildAddrString(string StreetAddress, string addrLine2, string city, string prov, string postalCode)
        {
            string fullAddr = "";

            if (!string.IsNullOrWhiteSpace(StreetAddress))
            {
                fullAddr += StringUtils.FirstCharToUpper(StreetAddress) + " ";
            }

            if (!string.IsNullOrWhiteSpace(addrLine2))
            {
                fullAddr += StringUtils.FirstCharToUpper(addrLine2) + ", ";
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                fullAddr += StringUtils.FirstCharToUpper(city) + ", ";
            }

            if (!string.IsNullOrWhiteSpace(prov))
            {
                fullAddr += StringUtils.FirstCharToUpper(prov) + ", ";
            }

            if (!string.IsNullOrWhiteSpace(postalCode))
            {
                fullAddr += StringUtils.FirstCharToUpper(postalCode);
            }

            return fullAddr;
        }

        
    }
}