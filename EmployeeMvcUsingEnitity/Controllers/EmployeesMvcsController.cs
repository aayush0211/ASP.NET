using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeMvcUsingEnitity.Models;

namespace EmployeeMvcUsingEnitity.Controllers
{
    public class EmployeesMvcsController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesMvcsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: EmployeesMvcs
        public async Task<IActionResult> Index()
        {
              return _context.EmployeesMvcs != null ? 
                          View(await _context.EmployeesMvcs.ToListAsync()) :
                          Problem("Entity set 'EmployeeContext.EmployeesMvcs'  is null.");
        }

        // GET: EmployeesMvcs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.EmployeesMvcs == null)
            {
                return NotFound();
            }

            var employeesMvc = await _context.EmployeesMvcs
                .FirstOrDefaultAsync(m => m.Name == id);
            if (employeesMvc == null)
            {
                return NotFound();
            }

            return View(employeesMvc);
        }

        // GET: EmployeesMvcs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeesMvcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,City,Address")] EmployeesMvc employeesMvc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeesMvc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeesMvc);
        }

        // GET: EmployeesMvcs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.EmployeesMvcs == null)
            {
                return NotFound();
            }

            var employeesMvc = await _context.EmployeesMvcs.FindAsync(id);
            if (employeesMvc == null)
            {
                return NotFound();
            }
            return View(employeesMvc);
        }

        // POST: EmployeesMvcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,City,Address")] EmployeesMvc employeesMvc)
        {
            if (id != employeesMvc.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeesMvc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesMvcExists(employeesMvc.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeesMvc);
        }

        // GET: EmployeesMvcs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.EmployeesMvcs == null)
            {
                return NotFound();
            }

            var employeesMvc = await _context.EmployeesMvcs
                .FirstOrDefaultAsync(m => m.Name == id);
            if (employeesMvc == null)
            {
                return NotFound();
            }

            return View(employeesMvc);
        }

        // POST: EmployeesMvcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.EmployeesMvcs == null)
            {
                return Problem("Entity set 'EmployeeContext.EmployeesMvcs'  is null.");
            }
            var employeesMvc = await _context.EmployeesMvcs.FindAsync(id);
            if (employeesMvc != null)
            {
                _context.EmployeesMvcs.Remove(employeesMvc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesMvcExists(string id)
        {
          return (_context.EmployeesMvcs?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
