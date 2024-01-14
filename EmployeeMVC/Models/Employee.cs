
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeMVC.Models
{
    public class Employee
    {
        public string? Name { get; set; }   
        
        public string? City { get; set; }

        public string? Address { get; set; }
    }

    public class EmployeeUtils : IEmployee, IDisposable
    {
       private static SqlConnection conn;
         static EmployeeUtils() { 
                  conn = Connection.getConnection();
            }
        public static void AddEmployee(Employee employee)
        {
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "AddEmployee";
            sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
            sqlCommand.Parameters.AddWithValue("@City", employee.City);
            sqlCommand.Parameters.AddWithValue("@Address",employee.Address);

            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                throw;
            }
        }

        public static String DeleteEmployee(string name)
        {
            SqlTransaction sqlTransaction = conn.BeginTransaction();    
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "DeleteEmployee";
            sqlCommand.Parameters.AddWithValue ("@Name", name);

            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                String msg = "Delete Successfully!!!";
                return msg;
            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                throw;
            }

        }

        public static List<Employee> getAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.Connection = conn;

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "GetAllEmployees";

            try
            {
                using (SqlDataReader dr = sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Employee employee = new Employee();
                        employee.Name = Convert.ToString(dr["Name"]);
                        employee.City = Convert.ToString(dr["City"]);
                        employee.Address = Convert.ToString(dr["Address"]);
                        employees.Add(employee);
                    }
                    return employees;
                }
            }catch (Exception)
            {
                throw;
            }
        }

        public static string UpdateEmployee(Employee employee)
        {
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.Transaction = sqlTransaction;

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "UpdateEmployee";
            sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
            sqlCommand.Parameters.AddWithValue("@City", employee.City);
            sqlCommand.Parameters.AddWithValue("@Address", employee.Address);

            
            try
            {
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                string msg = "Successfully Added";
                return msg;
            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                throw;
            }
        }

        public static Employee GetEmployee(String name)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from EmployeesMVC where Name = @Name";
            cmd.Parameters.AddWithValue("@Name", name);

            try
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    Employee employee = new Employee();
                    if (dr.Read())
                    {
                        employee.Name = Convert.ToString(dr["Name"]);
                        employee.City = Convert.ToString(dr["City"]);
                        employee.Address = Convert.ToString(dr["Address"]);
                        return employee;
                    }
                    else
                    {
                        return null;
                    }
                }
            }catch (Exception)
            {
                throw;
            }
        }
        public void Dispose()
        {
            conn.Close();
        }
    }
}
