
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample27.Data;
using Sample27.Models;
using SolrNet.Utils;

namespace Sample27.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _employeeContext;
        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            this._employeeContext = employeeDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var employee = await _employeeContext.Employees.ToListAsync();
            return View(employee);
        }
        // GET: EmployeeController



        // For the save data in database
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeData employees)
        {
            if (ModelState.IsValid)
            {
                _employeeContext.Employees.Add(employees);
                await _employeeContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employees);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _employeeContext.Employees == null)
            {
                return NotFound();
            }

            var employee = await _employeeContext.Employees.FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_employeeContext.Employees == null)
            {
                return Problem("Entity set 'EmployeeContext.Employees'  is null.");
            }
            var employee = await _employeeContext.Employees.FindAsync(id);
            if (employee != null)
            {
                _employeeContext.Employees.Remove(employee);
            }

            await _employeeContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var employeeInDb = await _employeeContext.Employees.SingleOrDefaultAsync(e => e.Id == id);

            if (employeeInDb == null)
            {
                return NotFound();
            }

            return View(employeeInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(EmployeeData employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            _employeeContext.Employees.Update(employee);
            await _employeeContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

         [HttpGet]
        public async Task<IActionResult> Index(string searchString)

        {
            ViewData["CreateNewEmployee"] = searchString;
            var movies = from s in _employeeContext.Employees select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.FName!.Contains(searchString) || s.City!.Contains(searchString) || s.LName!.Contains(searchString));
            }

            return View(await movies.AsNoTracking().ToListAsync());
        }






    }
}

