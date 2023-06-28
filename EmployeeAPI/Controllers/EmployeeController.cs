using EmployeeAPI.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Web.Http;

namespace EmployeeAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PostEmployee(Employee emp)
        {
            if (emp != null)
            {
                EmployeeBL employeeBL = new EmployeeBL();
                employeeBL.InsertEmployee(emp);
                return Ok("Record inserted successfully!");
            }
            else
            {
                return BadRequest("There is a problem with data. Plese add all input fields.");
            }
        }

        [HttpGet]
        public IHttpActionResult GetTax(int salary)
        {
            int totalTax = 0;
            if (salary > 0)
            {
                EmployeeBL employeeBL = new EmployeeBL();
                totalTax= employeeBL.GetTax(salary);
                return Ok(salary);
            }
            else
            {
                return BadRequest("Salary must greter than 0");
            }
        }
    }
}
