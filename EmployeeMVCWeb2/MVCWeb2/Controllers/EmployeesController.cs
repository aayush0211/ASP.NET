using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWeb2.Models;
using System.Text.Json;

namespace MVCWeb2.Controllers
{
    public class EmployeesController : Controller
    {
        
        // GET: Employee/Index
        public ActionResult Index()
        {
            
            List<Employee> employees = EmployeeUtill.GetAllEmployee();

            foreach (Employee emp in employees)
            {
                String jsonEmp = JsonSerializer.Serialize<Employee>(emp);
                HttpContext.Session.SetString($"{emp.EmpId}", "jsonEmp");
                HttpContext.Session.SetString("jsonEmp", emp.EmpId.ToString());
            }

            return View(employees);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            Employee emp = new Employee();  
            try
            {
                // 
                string? key = HttpContext.Session.GetString("jsonEmp");
                 emp = EmployeeUtill.GetEmployeeDetails(Convert.ToInt32(key));
                emp = JsonSerializer.Deserialize<Employee>(key!)!;
                return View(emp);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                EmployeeUtill.createNewEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = EmployeeUtill.GetEmployeeDetails(id);
            return View(employee);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee, int id)
        {
            try
            {
                Console.WriteLine(employee.EmpName);
                EmployeeUtill.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee =  EmployeeUtill.GetEmployeeDetails(id);
            return View(employee);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Employee employee, int id)
        {
            try
            {
                EmployeeUtill.DeleteEmployee(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
