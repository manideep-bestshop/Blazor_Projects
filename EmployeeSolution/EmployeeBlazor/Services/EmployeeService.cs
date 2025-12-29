using EmployeeBlazor.Models;

namespace EmployeeBlazor.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _http;

        public EmployeeService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Employee>> GetEmployees()
        {

            return await _http.GetFromJsonAsync<List<Employee>>("api/employee");
        }

        public async Task AddEmployee(Employee emp)
        {
            await _http.PostAsJsonAsync("api/employee", emp);
        }
        public async Task UpdateEmployee(Employee emp)
        {
            await _http.PutAsJsonAsync($"api/employee/{emp.Id}", emp);
        }

        public async Task DeleteEmployee(int id)
        {
            await _http.DeleteAsync($"api/employee/{id}");
        }
    }
}
