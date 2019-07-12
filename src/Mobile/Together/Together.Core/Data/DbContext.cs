using SQLite;
using System.Threading.Tasks;

namespace Together.Core.Data
{
    public class DbContext
    {
        private readonly SQLiteAsyncConnection _connection;
        public DbContext(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);

            InitTablesAsync().Wait();
        }

        public async Task InitTablesAsync()
        {
            await _connection.CreateTableAsync<StartUp>();
        }


        public Task<StartUp> GetStartUpAsync() 
            => _connection.Table<StartUp>().FirstOrDefaultAsync();
    }
}
