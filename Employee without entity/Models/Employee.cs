using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is Required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "City is Required.")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Address is Required.")]
        public string? Address { get; set; }
    }

    public class EmployeeUtils : IEmployee, IDisposable
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
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@City", employee.City);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception)
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
            cmd.Parameters.AddWithValue("@Id", id);
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

        public static List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GetAllEmployees";

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                       emp.Id = Convert.ToInt32(reader["Id"]);
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
        public static Employee GetEmployee(int id)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
          
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Select * from Employee where Id=@Id";
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Employee emp = new Employee();
                    if (reader.Read())
                    {
                        emp.Id = Convert.ToInt32(reader["Id"]);
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
            cmd.Parameters.AddWithValue("@Id", employee.Id);
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

        public void Dispose()
        {
            cn.Close();
        }
    }
}
