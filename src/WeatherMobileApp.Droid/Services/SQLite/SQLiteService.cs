using System;
using System.IO;
using WeatherMobileApp.Core.Services.SqLite;

namespace WeatherMobileApp.Droid.Services.SQLite
{
    public class SQLiteService : ISQLite
    {
        public string GetDatabasePath(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            return path;
        }
    }
}
