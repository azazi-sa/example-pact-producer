using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pact.Example.Provider.Models;

namespace Pact.Example.Provider.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // GET api/employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
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

        // GET api/employees/1
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            return new Employee
            {
                Id = "1",
                Name = "Bhagwan Ram: The God"
            };
        }

        // POST api/employees
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            return new Employee
            {
                Id = "1"
            };
        }

        // PUT api/employees/1
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
        }
    }
}
