using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories.Employees;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _employeeRepo;
        private readonly DbConnector _dbConnector;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
            _dbConnector = new DbConnector(Startup.Configuration.GetConnectionString("Default"));
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var employees = await _employeeRepo.GetAllAsync();
            //var employees = await _employeeRepo.GetDataSet("GetEmployees", System.Data.CommandType.Text);
            return Newtonsoft.Json.JsonConvert.SerializeObject(employees);
        }

        [HttpGet("GetEmployeesBySP")]
        public IEnumerable<Employee> GetEmployeesBySP()
        {
            var employees = new List<Employee>();

            using (var command = new SqlCommand("GetEmployees", _dbConnector.Connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                        });
                    }
                }
            }

            return employees;
        }


        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _employeeRepo.InsertAsync(employee);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee employee)
        {
            _employeeRepo.UpdateAsync(employee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeRepo.DeleteAsync(id);
            return Ok();
        }
    }
}