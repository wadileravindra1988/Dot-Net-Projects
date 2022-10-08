using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.API.DataContext;

namespace WebApplication.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ApplicationDbContext _DbContext;
         public EmployeeRepository(ApplicationDbContext dbContext)
            {
            _DbContext = dbContext;    
            }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _DbContext.AddAsync(employee);
            await _DbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var result = await _DbContext.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                 _DbContext.Employees.Remove(result);
                await _DbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await  _DbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await _DbContext.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _DbContext.Employees.FirstOrDefaultAsync(a => a.Id == employee.Id);
            if (result != null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                result.EmailId = employee.EmailId;
                await _DbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
