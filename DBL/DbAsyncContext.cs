using System;
using System.Threading.Tasks;
using Model.DbItems;
using SQLite;

namespace DBL
{
    public class DbAsyncContext : IDbAsyncContext
    {
        public SQLiteAsyncConnection Connection { get; private set; }

        public string DatabasePath { get; }

        public DbAsyncContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        public async Task ConnectToDatabase()
        {
            Connection = new SQLiteAsyncConnection(DatabasePath);

            await CreateTables()
                .ConfigureAwait(false);
        }

        public async Task CreateTables()
        {
            await Connection.CreateTablesAsync<CountryDbItem, WeatherDbItem>()
                .ConfigureAwait(false);
        }

        public async Task DropDatabase()
        {
            if (Connection is null)
            {
                throw new NullReferenceException("No Connection with database");
            }

            await Connection.DropTableAsync<CountryDbItem>()
                .ConfigureAwait(false);
            await Connection.DropTableAsync<WeatherDbItem>()
                .ConfigureAwait(false);
        }
    }
}
