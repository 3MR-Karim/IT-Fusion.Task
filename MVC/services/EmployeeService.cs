using MVC.Models;

namespace MVC.services
{
  

        public class EmployeeService
        {
            private readonly HttpClient _httpClient;

            public EmployeeService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<EmployeeViewModel>> GetAllAsync()
            {
                return await _httpClient.GetFromJsonAsync<List<EmployeeViewModel>>("https://localhost:7070/api/employees");
            }

            public async Task<EmployeeViewModel?> GetByIdAsync(int id)
            {
                return await _httpClient.GetFromJsonAsync<EmployeeViewModel>($"https://localhost:7070/api/employees/{id}");
            }

            public async Task<bool> AddAsync(EmployeeViewModel employee)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7070/api/employees", employee);
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> UpdateAsync(EmployeeViewModel employee)
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7070/api/employees/{employee.Id}", employee);
                return response.IsSuccessStatusCode;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7070/api/employees/{id}");
                return response.IsSuccessStatusCode;
            }
        }
    }


