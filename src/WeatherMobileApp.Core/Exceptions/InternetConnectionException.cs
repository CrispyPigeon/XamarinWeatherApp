using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherMobileApp.Core.Exceptions
{
   public class InternetConnectionException : Exception
    {
        public InternetConnectionException()
        { }

        public InternetConnectionException(string message)
            : base(message)
        { }

        public InternetConnectionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
