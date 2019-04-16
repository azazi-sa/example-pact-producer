using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pact.Example.Producer.Controllers
{
    [Produces("application/json")]
    [Route("employees")]
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = "1",
                    Name = "Bhagwan Ram: The God"
                }
            };
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return new Employee
            {
                Id = "1",
                Name = "Bhagwan Ram: The God"
            };
        }

        [HttpPost]
        public Employee Post([FromBody] Employee employee)
        {
            return new Employee
            {
                Id = "1"
            };
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
        }
    }

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}