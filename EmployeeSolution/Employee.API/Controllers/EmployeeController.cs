using Dapper;
using Microsoft.Data.SqlClient;
using Employee.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employee.API.Models;

namespace Employee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class EmployeeController : ControllerBase
    {
        private readonly DapperContext _context;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(DapperContext context, ILogger<EmployeeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            _logger.LogInformation("Fetching all employees");

            var query = "SELECT * FROM Employees";
            using var connection = _context.CreateConnection();
            var employees = await connection.QueryAsync<Employee.API.Models.Employee>(query);

            _logger.LogInformation("Fetched {Count} employees", employees.Count());
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee.API.Models.Employee emp)
        {
            var query = @"INSERT INTO Employees 
                          (Name, Email, Department, Salary)
                          VALUES (@Name, @Email, @Department, @Salary)";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, emp);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Models.Employee emp)
        {
            var query = @"
        UPDATE Employees
        SET Name = @Name,
            Email = @Email,
            Department = @Department,
            Salary = @Salary
        WHERE Id = @Id";

            emp.Id = id;

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, emp);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var query = "DELETE FROM Employees WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });

            return Ok();
        }

    }
}
