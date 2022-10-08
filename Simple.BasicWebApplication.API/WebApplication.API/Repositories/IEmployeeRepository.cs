using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.API.Repositories
{
    public interface IEmployeeRepository
    {
        //Task use because we will return entity once task complete like get employee , add employee
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int Id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee>  DeleteEmployee(int Id);
    }
}
