using System.Text.Json;

namespace Employee.API.Services
{
    public class EmployeeJsonService
    {
        private readonly string _filePath = "Data/employees.json";
        private readonly object _lock = new();

        public List<EmployeeModel> GetAll()
        {
            if (!File.Exists(_filePath))
                return new List<EmployeeModel>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<EmployeeModel>>(json) ?? new List<EmployeeModel>();
        }

        public EmployeeModel? GetById(int id)
        {
            return GetAll().FirstOrDefault(e => e.Id == id);
        }

        public void Add(EmployeeModel employee)
        {
            var employees = GetAll();
            employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
            employees.Add(employee);
            SaveAll(employees);
        }


        public void Update(EmployeeModel employee)
        {
            var employees = GetAll();
            var existing = employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existing != null)
            {
                existing.Name = employee.Name;
                existing.JoinDate = employee.JoinDate;
                existing.Phone = employee.Phone;
                existing.Gender = employee.Gender;
                existing.Salary = employee.Salary;
                SaveAll(employees);  
            }
        }


        public void Delete(int id)
        {
            var employees = GetAll();
            var toRemove = employees.FirstOrDefault(e => e.Id == id);
            if (toRemove != null)
            {
                employees.Remove(toRemove);
                SaveAll(employees);
            }
        }

        private void SaveAll(List<EmployeeModel> employees)
        {
            lock (_lock)
            {
                var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
                File.WriteAllText(_filePath, json);
            }
        }
    }
}
