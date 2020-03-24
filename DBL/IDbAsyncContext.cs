using System.Threading.Tasks;
using SQLite;

namespace DBL
{
    public interface IDbAsyncContext
    {
        SQLiteAsyncConnection Connection { get; }

        Task ConnectToDatabase();
        Task CreateTables();
        Task DropDatabase();
    }
}
