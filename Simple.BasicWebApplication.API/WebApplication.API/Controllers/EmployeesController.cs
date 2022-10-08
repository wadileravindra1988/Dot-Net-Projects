using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.API.Repositories;

namespace WebApplication.API.Controllers
{
    //In mvc COntroler i herite from Controller but in api we will use from ControllerBAse
    [Route("api/[controller]")] // notation // when we use / controlller then no need to use ccontroller when we call EmployeeController we can call direct using Employee
    [ApiController] //notaion
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
            {
                try
                {
                    return Ok(await _employeeRepository.GetEmployees());

                }
                catch (Exception)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError,"Error in Retrving Data");
                }    
            }
        [HttpGet ("{id:int}") ]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Data Found");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var result = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new {id= result.Id},result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Data Found");
            }
        }

        [HttpPut ("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id,Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Employee Id mismtach");
                }
                    var employeeUpdate = await _employeeRepository.GetEmployee(id);
                    if (employeeUpdate == null)
                    {
                        return NotFound($"Employee id={id} not found");
                    } 
                    return await _employeeRepository.UpdateEmployee(employee);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Data Found");
            }
        }

        [HttpDelete ("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeUpdate = await _employeeRepository.GetEmployee(id);
                if (employeeUpdate == null)
                {
                    return NotFound($"Employee id={id} not found");
                }
                return await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Data Found");
            }
        }
    }
}
