using System;
using System.IO;
using WeatherMobileApp.Core.Services.SqLite;

namespace WeatherMobileApp.iOS.Services.SQLite
{
    public class SQLiteService : ISQLite
    {
        public const string Dots = "..";
        public const string Library = "Library";

        public string GetDatabasePath(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, Dots, Library);

            return Path.Combine(libraryPath, filename);
        }
    }
}
