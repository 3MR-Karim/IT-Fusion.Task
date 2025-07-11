using Employee.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using EmployeeModel = Employee.MVC.Models.Employee;

namespace Employee.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/employees");
            var json = await response.Content.ReadAsStringAsync();
            var employees = JsonSerializer.Deserialize<List<EmployeeModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel employee)
        {
            var json = JsonSerializer.Serialize(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/employees", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/employees/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var employee = JsonSerializer.Deserialize<EmployeeModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel employee)
        {
            var json = JsonSerializer.Serialize(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/employees/{employee.Id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/employees/{id}");

            return RedirectToAction("Index");
        }
    }
}
