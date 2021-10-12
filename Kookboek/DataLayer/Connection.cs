using System.Threading.Tasks;
using Npgsql;

namespace DataLayer
{
    public static class Connection
    {
        internal static string connString = "Host=localhost;Username=appuser;Password=1234;Database=kookboek";
    }
}