using EmployeeMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
        public ActionResult GetAllEmployees()
        {
            List<Employee> employees = EmployeeUtils.getAllEmployees();   
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(String name)
        {
            String msg;
            Employee emp = EmployeeUtils.GetEmployee(name);
            if (emp != null)
            {
                return View(emp);
            }
            msg = "Unsuccessfull Operation!!!";
            TempData.Add("msg",msg);
            return RedirectToAction(nameof(GetAllEmployees));
        }

        // GET: EmployeeController/Create
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        public ActionResult AddEmployee(Employee emp)
        {
            String msg;
            try
            {
                EmployeeUtils.AddEmployee(emp);
                msg = "Succesfully Added";
                TempData.Add("msg",msg);    
                return RedirectToAction(nameof(GetAllEmployees));
            }
            catch
            {
                msg = "Unsuccessfull Operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployees));
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult UpdateEmployee(String name)
        {
            String msg;
            Employee emp = EmployeeUtils.GetEmployee(name);
            if (emp != null)
            {
                return View(emp);
            }

            msg = "Unsuccessfull operation!!!";
            TempData.Add("msg", msg);
            return RedirectToAction(nameof(GetAllEmployees));
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        public ActionResult UpdateEmployee(String name, Employee emp)
        {
            String msg;
            try
            {
               msg = EmployeeUtils.UpdateEmployee(emp);
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployees));
            }
            catch
            {
                msg = "Unsuccessfull operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployees));
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(String name)
        {
            String msg;
            Employee emp = EmployeeUtils.GetEmployee(name);
            if (emp != null)
            {
                return View(emp);
            }
            msg = "Unsuccessfull Operation!!!";
            TempData.Add("msg", msg);
            return RedirectToAction(nameof(GetAllEmployees));
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        public ActionResult Delete(String name, Employee emp)
        {
            String msg;
            try
            { 
              msg = EmployeeUtils.DeleteEmployee(name);
                TempData.Add("msg" , msg);
                return RedirectToAction(nameof(GetAllEmployees));
            }
            catch
            {
                msg = "Unsuccessfull Operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployees));
            }
        }
    }
}
