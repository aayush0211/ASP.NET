using EmployeeManagementMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
        public ActionResult Index()
        {
            List<Employee> list = EmployeeUtils.GetAll();
            return View(list);
        }

        // GET: EmployeeController/Details/5
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
            return RedirectToAction(nameof(Index));
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp)
        {

            String msg;
            try
            {
                EmployeeUtils.AddEmployee(emp);
                msg = "Successfully added!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                msg = "Unsuccessfull operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            String msg;
            Employee emp = EmployeeUtils.GetEmployee(id);
            if (emp != null)
            {
                return View(emp);
            }

            msg = "Unsuccessfull operation!!!";
            TempData.Add("msg", msg);
            return RedirectToAction(nameof(Index));
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee emp)
        {
            String msg;
            try
            {
                msg = EmployeeUtils.UpdateEmployee(emp);
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                msg = "unsuccessfull operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: EmployeeController/Delete/5
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
            return RedirectToAction(nameof(Index));
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee emp)
        {
            string msg;
            try
            {
                msg = EmployeeUtils.DeleteEmployee(id);
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                msg = "Unsuccessfull operation!!!";
                TempData.Add("msg", msg);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
