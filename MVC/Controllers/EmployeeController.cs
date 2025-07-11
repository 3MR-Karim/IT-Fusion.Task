using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.services;
using System.Diagnostics;

namespace MVC.Controllers
{
  
    namespace Employee.MVC.Controllers
    {
        public class EmployeeController : Controller
        {
            private readonly  EmployeeService _service;

            public EmployeeController(EmployeeService service)
            {
                _service = service;
            }

            public async Task<IActionResult> Index()
            {
                var employees = await _service.GetAllAsync();
                return View(employees);
            }

            public IActionResult Create() => View();

            [HttpPost]
            public async Task<IActionResult> Create(EmployeeViewModel employee)
            {
                if (!ModelState.IsValid) return View(employee);

                await _service.AddAsync(employee);
                return RedirectToAction("Index");
            }

            public async Task<IActionResult> Edit(int id)
            {
                var employee = await _service.GetByIdAsync(id);
                if (employee == null) return NotFound();
                return View(employee);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(EmployeeViewModel employee)
            {
                if (!ModelState.IsValid) return View(employee);

                await _service.UpdateAsync(employee);
                return RedirectToAction("Index");
            }

            public async Task<IActionResult> Delete(int id)
            {
                var employee = await _service.GetByIdAsync(id);
                if (employee == null) return NotFound();
                return View(employee);
            }

            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
        }
    }

}
