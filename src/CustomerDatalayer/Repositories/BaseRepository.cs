using System.Data.SqlClient;

namespace  CustomerDatalayer.Repositories
{
    public class BaseRepository
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection("Server=localhost\\SQLEXPRESS;Database=CustomerLib_Shikanov;Trusted_Connection=True;");
        }
    }
}