using Microsoft.Data.SqlClient;
using Employee_Management_System.Models;

namespace Employee_Management_System.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
    public  class EmployeeUtils :IDisposable, Interface
    {
        private static SqlConnection cn;
         static EmployeeUtils()
        {
            cn = Connection.GetConnection();
        }

        public static void AddEmployee(Employee employee)
        {
            SqlTransaction tran = cn.BeginTransaction();
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = tran;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "AddEmployee";
            cmd.Parameters.AddWithValue("@Name",employee.Name);
            cmd.Parameters.AddWithValue("@City", employee.City); 
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
            }catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }

        public static string DeleteEmployee(int id)
        {
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = tran;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "DeleteEmployee";
            cmd.Parameters.AddWithValue("@EmployeeId", id);
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                String msg = "Delete Successfully!!!";
            return msg;
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
           cn.Close();
        }
        public static Employee GetEmployee(int id)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
           // cmd.Transaction = tran;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Select * from tbl_Employee where EmployeeId=@EmployeeId";
            cmd.Parameters.AddWithValue("@EmployeeId", id);
            
            try
            {
               using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    Employee emp = new Employee();
                    if (reader.Read())
                    {
                        emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                        emp.Name = Convert.ToString(reader["Name"]);
                        emp.City = Convert.ToString(reader["City"]);
                        emp.Address = Convert.ToString(reader["Address"]);
                        return emp;
                    }
                    else
                    {
                        return null;

                    }
                }
              //  tran.Commit();
            }
            catch (Exception)
            {
             //   tran.Rollback();
                throw;
            }
        }

        public static List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>(); 
           // SqlTransaction tran = cn.BeginTransaction();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
          //  cmd.Transaction = tran;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GetAllEmployees";
            
            try
            {               
              using(SqlDataReader reader = cmd.ExecuteReader())
                {
                 //   tran.Commit();
                 while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeId= Convert.ToInt32(reader["EmployeeId"]);
                        emp.Name = Convert.ToString(reader["Name"]);
                        emp.City = Convert.ToString(reader["City"]);
                        emp.Address = Convert.ToString(reader["Address"]);
                        list.Add(emp);
                    }
                   return list;

                }
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static string UpdateEmployee(Employee employee)
        {
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = tran;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "UpdateEmployee";
            cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@City", employee.City);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                String msg = "successfully updated!!!";
                return msg;
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
