using Microsoft.Data.SqlClient;

namespace EmployeeMVC.Models
{
    public class Connection
    {
        public static SqlConnection getConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Employee;Integrated Security=True;";
            conn.Open();
            return conn;
        }
    }
}
