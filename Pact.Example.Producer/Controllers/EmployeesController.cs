using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Pact.Example.Producer.Controllers
{
    [Produces("application/json")]
    [Route("employees")]
    public class EmployeesController : Controller
    {
        [HttpGet]
        public List<Employee> Get()
        {
            HttpContext.Response.Headers.Add("Content-Type", "application/json");
            return new List<Employee>
            {
                new Employee
                {
                    Id = "1",
                    Name = "Ram Shinde"
                }
            };
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            HttpContext.Response.Headers.Add("Content-Type", "application/json");
            return new Employee
            {
                Id = "1",
                Name = "Ram Shinde"
            };
        }

        [HttpPut]
        public Employee Put([FromBody] Employee employee)
        {
            HttpContext.Response.Headers.Add("Content-Type", "application/json");
            return new Employee
            {
                Id = "1"
            };
        }

        [HttpPost("{id}")]
        public Employee Post(int id, [FromBody] Employee employee)
        {
            HttpContext.Response.Headers.Add("Content-Type", "application/json");
            return employee;
        }
    }

    public class Employee
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
    }
}