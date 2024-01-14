using Collections;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Databases
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Connect();
            //List<Department> departmentList = new List<Department>();
            //departmentList.Add(new Department { DeptId = 1, DepartmentName = "Marketing" });
            //departmentList.Add(new Department { DeptId = 2, DepartmentName = "RnD" });
            //departmentList.Add(new Department { DeptId = 3, DepartmentName = "Sales" });

            //foreach (Department department in departmentList)
            //{
            //    AddDepartment(department);
            //}


            DataReader1();
            List<Employee> employees = GetAllEmployee();
            foreach(Employee emp in employees)
            {
                Console.WriteLine(emp);
            }

        }


        //By Try Catch    
        static void Connect()
        {
            SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = Data Source=(localdb)\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
            conn.ConnectionString = @"Data Source = (localdb)\ProjectModels; Initial Catalog = ActsDec2023; Integrated Security = True;";
            try
            {
                conn.Open();
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { conn.Close(); }

        }

        //By Using
        static void Connect2()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"Data Source = (localdb)\ProjectModels; Initial Catalog = ActsDec2023; Integrated Security = True;";
                try
                {
                    conn.Open();
                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        //using try Catch
        static void Insert1()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (localdb)\ProjectModels; Initial Catalog = ActsDec2023; Integrated Security = True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "insert into values (1,'Aayush', 100000,1)";
                cmdInsert.ExecuteNonQuery(); //xecutes a Transact-SQL statement against the connection and returns the number of rows affected.

                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { cn.Close(); }
        }

      
        static void AddEmployee1(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (localdb)\ProjectModels; Initial Catalog = ActsDec2023; Integrated Security = True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert2 = new SqlCommand();
                cmdInsert2.Connection = cn;
                cmdInsert2.CommandType = CommandType.Text;
                cmdInsert2.CommandText = $"insert into Employees values({obj.EmpId},{obj.Name},{obj.BasicPay},{obj.DeptId})";
                cmdInsert2.ExecuteNonQuery();
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { cn.Close(); }
        }
        //PARAMETERS
        static void AddEmployee2(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (localdb)\ProjectModels; Initial Catalog = ActsDec2023; Integrated Security = True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = "insert into Employees values (@EmpId, @EmpName, @Basic, @DeptNo)";
                cmdInsert.Parameters.AddWithValue("@EmpId", obj.EmpId);
                cmdInsert.Parameters.AddWithValue("@EmpName", obj.Name);
                cmdInsert.Parameters.AddWithValue("@Basic", obj.BasicPay);
                cmdInsert.Parameters.AddWithValue("@DeptNo", obj.DeptId);
                cmdInsert.ExecuteNonQuery();

                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                cn.Close();
            }

        }

        static void AddDepartment(Department dept)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;";

            cn.Open();
            Console.WriteLine("Success");
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand cmdInsertDepart = new SqlCommand();
            cmdInsertDepart.Connection = cn;
            cmdInsertDepart.Transaction = tran;

            cmdInsertDepart.CommandType = System.Data.CommandType.StoredProcedure;
            cmdInsertDepart.CommandText = "dbo.InsertDepartment";

            cmdInsertDepart.Parameters.AddWithValue("@DeptNo",dept.DeptId);
            cmdInsertDepart.Parameters.AddWithValue("@DeptName", dept.DepartmentName);

            try
            {
                cmdInsertDepart.ExecuteNonQuery();
                tran.Commit();
                Console.WriteLine("no errors- commit");
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine(ex.Message);
            }
            cn.Close();
        }
        static void AddEmployee0(Employee obj)
        {

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;";

            cn.Open();
            SqlTransaction tran = cn.BeginTransaction();

            SqlCommand cmdInsert = new SqlCommand();

            cmdInsert.Connection = cn;
            cmdInsert.Transaction = tran;

            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.CommandText = "dbo.InsertEmployee";

            cmdInsert.Parameters.AddWithValue("@EmpId", obj.EmpId);
            cmdInsert.Parameters.AddWithValue("@EmpName", obj.Name);
            cmdInsert.Parameters.AddWithValue("@Basic", obj.BasicPay);
            cmdInsert.Parameters.AddWithValue("@DeptNo", obj.DeptId);

            try
            {
                cmdInsert.ExecuteNonQuery();
                tran.Commit();
                Console.WriteLine("no errors- commit");
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine(ex.Message);
            }
            cn.Close();

        }

        public static void DeleteEmployee0(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;";

            cn.Open();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmdDelete = new SqlCommand();
            cmdDelete.Connection = cn;
            cmdDelete.Transaction = tran;

            cmdDelete.CommandType = CommandType.Text;
            cmdDelete.CommandText = "delete from Employees where EmpId = @EmpId";
            try
            {
                cmdDelete.ExecuteNonQuery();
                tran.Commit();
                Console.WriteLine("no errors- commit");
            }
            catch (Exception ex) { tran.Rollback(); Console.WriteLine(ex.Message); }
            cn.Close();
        }
        public static void updateEmployee(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;";
            cn.Open();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = cn;
            cmdUpdate.Transaction = tran;

            cmdUpdate.CommandType = CommandType.Text;
            cmdUpdate.CommandText = "update Employee set Basic = @Basic";
            try
            {
                cmdUpdate.ExecuteNonQuery();
                tran.Commit();
                Console.WriteLine("no errors- commit");
            }
            catch (Exception ex) { tran.Rollback(); Console.WriteLine(ex.Message); }
            cn.Close();


        }

        public static void selectSingleValue()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select EmpName from Employees where EmpNo = 1";

                object retval = cmd.ExecuteScalar();
                Console.WriteLine(retval);
                Console.WriteLine("Success");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { 
                cn.Close(); 
            }

        }

        static void DataReader1()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;";

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Employees;select * from Departments ";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine(dr[1]);
                    Console.WriteLine(dr["EmpName"]);
                }
                Console.WriteLine();
                dr.NextResult();
                while (dr.Read())
                {
                    Console.WriteLine(dr["DeptName"]);
                }    
                dr.Close();
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }

        }
        static List<Employee> GetAllEmployee()
        {
            List<Employee> list = new List<Employee>(); 
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=ActsDec2023;Integrated Security=True;";
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Employees";
            using(SqlDataReader dr = cmd.ExecuteReader()) { 
            while (dr.Read()) { 
                    string name = dr.GetString("EmpName");
                    int id = Convert.ToInt32(dr["EmpId"]);
                    Decimal basic = dr.GetDecimal("Basic");
                    short deptNo = dr.GetInt16("DeptNo");
                    Employee e =new Employee(name,id,basic,deptNo);
                    list.Add(e);
                }
            }

            return list;
        }
    }
}