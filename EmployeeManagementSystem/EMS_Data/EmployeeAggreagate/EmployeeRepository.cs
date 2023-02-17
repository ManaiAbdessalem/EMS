using EMS_Domain.EmployeeAggreagate;
using Microsoft.EntityFrameworkCore;

namespace EMS_Data.EmployeeAggreagate
{
    /// <summary>
    /// Class <see cref="EmployeeRepository"/> containing CRUD.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private EMSContext _EMSContext { get; set; }

        public EmployeeRepository(EMSContext EMSContext)
        {
            _EMSContext = EMSContext;
        }

        /// <inheritdoc />
        public async Task<Employee> CreateEmployee(Employee employee)
        {
            await _EMSContext.AddAsync<Employee>(employee);
            await _EMSContext.SaveChangesAsync();
            return employee;
        }

        /// <inheritdoc />
        public async Task<Employee> GetEmployee(int id)
        {
            return await _EMSContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task UpdateEmployee(Employee employee)
        {
            _EMSContext.Entry(employee).State = EntityState.Modified;
            await _EMSContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _EMSContext.Employees.ToListAsync();
        }

        /// <inheritdoc />
        public async Task DeleteEmployee(Employee employee)
        {
            _EMSContext.Employees.Remove(employee);
            await _EMSContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<Employee>> SearchEmployees(string text)
        {
            return await _EMSContext.Employees.Where(
                x=>x.Name.ToLower().Trim().Contains(text)||
                x.Department.ToLower().Trim().Contains(text)||
                x.Email.ToLower().Trim().Contains(text)
                ).ToListAsync();
        }
    }
}