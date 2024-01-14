﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiServerExample
{
    public class Employee
    {
        public int EmpNo { get; set; }
        public string Name{ get; set; }
        public decimal Basic { get; set; }
        public int DeptNo { get; set; }

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> lstEmps = new List<Employee>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IKS2;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "select * from employees ";
                SqlDataReader dr = cmdInsert.ExecuteReader();
                while (dr.Read())
                    lstEmps.Add(new Employee { EmpNo = dr.GetInt32(0), Name = dr.GetString(1), Basic = dr.GetDecimal(2), DeptNo = dr.GetInt32(3) });
                dr.Close();

            }
            catch (Exception ex)
            {
                throw ;
            }
            finally
            {
                cn.Close();
            }
            return lstEmps;
        }
        public static Employee GetSingleEmployee(int EmpNo)
        {
            Employee obj = new Employee();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IKS2;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "select * from employees where empno=@EmpNo";
                cmdInsert.Parameters.AddWithValue("@EmpNo", EmpNo);
                SqlDataReader dr = cmdInsert.ExecuteReader();
                if (dr.Read())
                {
                    obj.EmpNo = dr.GetInt32(0);
                    obj.Name = dr.GetString(1);
                    obj.Basic = dr.GetDecimal(2);
                    obj.DeptNo = dr.GetInt32(3);

                }
                else
                {
                    Console.WriteLine("Not present");
                    //record not persent
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
            //read from db for EmpNo = 1 and populate obj
            return obj;
        }
        public static void InsertEmployee(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IKS2;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "insert into Employees values(@EmpNo,@Name,@Basic,@DeptNo)";

                cmdInsert.Parameters.AddWithValue("@EmpNo", obj.EmpNo);
                cmdInsert.Parameters.AddWithValue("@Name", obj.Name);
                cmdInsert.Parameters.AddWithValue("@Basic", obj.Basic);
                cmdInsert.Parameters.AddWithValue("@DeptNo", obj.DeptNo);
                cmdInsert.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }


        }
        public static void UpdateEmployee(Employee obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IKS2;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "update employees set name=@Name, basic=@Basic, deptno=@Deptno where empno =@EmpNo";


                cmdInsert.Parameters.AddWithValue("@EmpNo", obj.EmpNo);
                cmdInsert.Parameters.AddWithValue("@Name", obj.Name);
                cmdInsert.Parameters.AddWithValue("@Basic", obj.Basic);
                cmdInsert.Parameters.AddWithValue("@DeptNo", obj.DeptNo);
                cmdInsert.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }


        }
        public static void DeleteEmployee(int EmpNo)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IKS2;Integrated Security=True";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "delete from employees where empno =@EmpNo";


                cmdInsert.Parameters.AddWithValue("@EmpNo", EmpNo);
                cmdInsert.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }


        }
    }
}
