using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Consts
    {
        //Administrative strings
        public const string BaseUrl = "http://api.openweathermap.org/data/2.5/";
        public const string ContentTypeJson = "application/json";

        //End points
        public const string Forecast = "forecast";

        //Keywords
        public const string AppId = "appid";
        public const string Longitude = "lon";
        public const string Latitude = "lat";
        public const string Units = "units";
        public const string Language = "lang";

        //Params
        public const string ApiKey = "0160dc60ff74dfca79aa310ee187d61e";
        public const string UnitCel = "metric";

    }
}
