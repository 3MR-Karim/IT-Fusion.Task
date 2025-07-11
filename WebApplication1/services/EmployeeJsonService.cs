using System.Text.Json;
using WebApplication1.Models;

namespace Employee.API.Services
{
    public class EmployeeJsonService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "employees.json");

        public EmployeeJsonService()
        {
            if (!Directory.Exists("Data"))
                Directory.CreateDirectory("Data");

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public List<Employee> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
        }

        public Employee? GetById(int id)
        {
            return GetAll().FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            var employees = GetAll();
            employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
            employees.Add(employee);
            Save(employees);
        }

        public void Update(Employee updated)
        {
            var employees = GetAll();
            var index = employees.FindIndex(e => e.Id == updated.Id);
            if (index == -1) return;
            employees[index] = updated;
            Save(employees);
        }

        public void Delete(int id)
        {
            var employees = GetAll().Where(e => e.Id != id).ToList();
            Save(employees);
        }

        private void Save(List<Employee> employees)
        {
            var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
