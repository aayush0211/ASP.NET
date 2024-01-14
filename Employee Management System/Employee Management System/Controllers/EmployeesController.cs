using Employee_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Employee_Management_System.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: EmployeesController
        public ActionResult GetAllEmployee()
        {
          List<Employee>list =  EmployeeUtils.GetAll();
            return View(list);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            String msg;
            Employee emp = EmployeeUtils.GetEmployee(id);
            if (emp != null)
            {
                return View(emp);
            }

            msg = "Unsuccessfull operation!!!";
            TempData.Add("msg", msg);
            return RedirectToAction(nameof(GetAllEmployee));
        }

        // GET: EmployeesController/Create
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
       
        public ActionResult AddEmployee(Employee emp)
        {
            String msg;
            try
            {
            EmployeeUtils.AddEmployee(emp);
                msg = "Successfully added!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployee));
            }
            catch
            {
                msg = "Unsuccessfull operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployee));
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult UpdateEmployee(int id)
        {
            String msg;
            Employee emp = EmployeeUtils.GetEmployee(id);
            if (emp != null)
            {
                return View(emp);
            }

                msg = "Unsuccessfull operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployee));
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        
        public ActionResult UpdateEmployee(int id, Employee emp)
        {
            String msg;
            try
            {
          msg = EmployeeUtils.UpdateEmployee(emp);
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployee));
            }
            catch
            {
                msg = "unsuccessfull operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployee)); 
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            String msg;
            Employee emp = EmployeeUtils.GetEmployee(id);
            if (emp != null)
            {
                return View(emp);
            }

            msg = "Unsuccessfull operation!!!";
            TempData.Add("msg", msg);
            return RedirectToAction(nameof(GetAllEmployee));
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee emp)
        {
            string msg;
            try
            {
                msg = EmployeeUtils.DeleteEmployee(id);
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployee));
            }
            catch
            {
                msg = "Unsuccessfull operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(GetAllEmployee));
            }
        }
    }
}
