using Employee.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeJsonService _service;

        public EmployeesController(EmployeeJsonService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get() => _service.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null) return NotFound();
            return employee;
        }

        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            _service.Add(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();

            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.Update(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.Delete(id);
            return NoContent();
        }
    }
}
