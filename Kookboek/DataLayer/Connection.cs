using System.Threading.Tasks;
using Npgsql;

namespace DataLayer
{
    public static class Connection
    {
        private static string connString = "Host=localhost;Username=appuser;Password=1234;Database=kookboek";

        internal static async Task<NpgsqlConnection> OpenConnection()
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            return conn;
        }
        
    }
}