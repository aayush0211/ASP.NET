using Microsoft.Data.SqlClient;
using System.Data;

namespace MVCWeb2.Models
{
    public class Employee
    {
        private static int id = 0;

        public int EmpId
        {
            get; set;
        }
        //private string? name;
        
        public string? EmpName
        {
            get; set;
        }

        private decimal basicPay;
        private int id1;
        private decimal basic;
        public short DeptNo
        {
            get; set;
        }

        public decimal BasicPay
        {
            get { return basicPay; }
            set
            {
                if (value < 1000)
                {
                    throw new Exception("Basic Pay is entered less!!");
                }
                else
                {
                    basicPay = value;
                }
            }

        }

        public String[] Skills
        {
            get; set;
        }
        public Employee() { }
        public Employee(String name = null!, decimal basicPay = 1000, String Skills = null!)
        {
            EmpId = ++id;
            EmpName = name;
            BasicPay = basicPay;
        }

        public Employee(string name, int id1, decimal basic, short deptNo)
        {
            EmpName = name;
            EmpId = id1;
            BasicPay = basic;
            DeptNo = deptNo;
        }
    }
    public class EmployeeUtill 
    {
        
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ActsDec2023;Integrated Security=True;";
            try
            {
                conn.Open();
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return conn;
        }
        public static void createNewEmployee(Employee emp)
        {

            SqlConnection cn = new SqlConnection();
            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ActsDec2023;Integrated Security=True;";
            cn.Open();

            SqlCommand sqlCommand = new SqlCommand();
                SqlTransaction tran = cn.BeginTransaction();
                sqlCommand.Connection = cn;
                sqlCommand.Transaction = tran;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            try { 
                sqlCommand.CommandText = "InsertEmployee";
                sqlCommand.Parameters.AddWithValue("@EmpId", emp.EmpId);
                sqlCommand.Parameters.AddWithValue("@EmpName", emp.EmpName);
                sqlCommand.Parameters.AddWithValue("@Basic", emp.BasicPay);
                sqlCommand.Parameters.AddWithValue("@DeptNo", emp.DeptNo);
                sqlCommand.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
              
            }  

        }
        public static Employee GetEmployeeDetails(int id)
        {
            Employee emp;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ActsDec2023;Integrated Security=True;";
            cn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = cn; 
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.CommandText = "select * from Employees where EmpId=@EmpId";
                sqlCommand.Parameters.AddWithValue("@EmpId", id);
                ////sqlCommand.Parameters.AddWithValue("@Name", emp.Name);
                ////sqlCommand.Parameters.AddWithValue("@Basic", emp.BasicPay);
                //sqlCommand.Parameters.AddWithValue("@DeptNo", emp.DeptNo);
                using (SqlDataReader dr = sqlCommand.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        emp = new Employee() { EmpId = dr.GetInt32("EmpId"), EmpName = dr.GetString("EmpName"), BasicPay = dr.GetDecimal("Basic"), DeptNo = dr.GetInt16("DeptNo") };
                    }
                    else
                    {
                        throw new Exception("Resource not found");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return emp;
        }

        public static void UpdateEmployee(Employee emp)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ActsDec2023;Integrated Security=True;";
            cn.Open();

            Console.WriteLine(emp.EmpName);

            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = cn;
            cmdUpdate.Transaction = tran;

            cmdUpdate.CommandType = CommandType.Text;
            cmdUpdate.CommandText = "update Employees set EmpName = @EmpName, Basic = @Basic, DeptNo=@DeptNo where EmpId = @EmpId";
            cmdUpdate.Parameters.AddWithValue("@EmpId", emp.EmpId);
            cmdUpdate.Parameters.AddWithValue("@EmpName", emp.EmpName);
            cmdUpdate.Parameters.AddWithValue("@Basic", emp.BasicPay);
            cmdUpdate.Parameters.AddWithValue("@DeptNo", emp.DeptNo);

            try
            {
                Console.WriteLine(cmdUpdate.ExecuteNonQuery());
                tran.Commit();
                //Console.WriteLine("no errors- commit");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                tran.Rollback(); 
            }


        }
        public static void DeleteEmployee(int emp)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ActsDec2023;Integrated Security=True;";
            cn.Open();

            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmdDelete = new SqlCommand();
            cmdDelete.Connection = cn;
            cmdDelete.Transaction = tran;

            cmdDelete.CommandType = CommandType.Text;
            cmdDelete.CommandText = "delete from Employees where EmpId = @EmpId";
            cmdDelete.Parameters.AddWithValue("@EmpId", emp);
            try
            {
                cmdDelete.ExecuteNonQuery();
                tran.Commit();
                
            }
            catch (Exception ex) { tran.Rollback();  }
            cn.Close();
        }
        public static List<Employee> GetAllEmployee()
        {
            List<Employee> list = new List<Employee>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ActsDec2023;Integrated Security=True;";
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Employees";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    string name = dr.GetString("EmpName");
                    int id = Convert.ToInt32(dr["EmpId"]);
                    Decimal basic = dr.GetDecimal("Basic");
                    short deptNo = dr.GetInt16("DeptNo");
                    Employee e = new Employee(name, id, basic, deptNo);
                    list.Add(e);
                }
            }

            return list;
        }

       
    }
    
}
