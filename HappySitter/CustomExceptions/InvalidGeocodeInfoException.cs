using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappySitter.CustomExceptions
{
    public class InvalidGeocodeInfoException : Exception
    {
        public InvalidGeocodeInfoException(string message)
            : base(message)
        {
        }
    }
}