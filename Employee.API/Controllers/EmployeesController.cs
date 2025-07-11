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
        public ActionResult<List<EmployeeModel>> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeModel> GetById(int id)
        {
            var emp = _service.GetById(id);
            if (emp == null) return NotFound();
            return emp;
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel employee)
        {
            _service.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, EmployeeModel employee)
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
