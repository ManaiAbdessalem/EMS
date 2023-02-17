namespace EMS_Domain.EmployeeAggreagate
{
    /// <summary>
    /// Interface <see cref="IEmployeeRepository"/> containing CRUD.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Creates an Employee.
        /// </summary>
        /// <param name="employee">employee input</param>
        /// <returns>employee</returns>
        Task<Employee> CreateEmployee(Employee employee);

        /// <summary>
        /// Updates an Employee.
        /// </summary>
        /// <param name="employee">employee to update</param>
        Task UpdateEmployee(Employee employee);

        /// <summary>
        /// Get Employee by Id
        /// </summary>
        /// <param name="id">id employee</param>
        /// <returns>Employee</returns>
        Task<Employee> GetEmployee(int id);

        /// <summary>
        /// Return all employees
        /// </summary>
        /// <returns>List of Employee</returns>
        Task<List<Employee>> GetAllEmployees();

        /// <summary>
        /// Delte an employee.
        /// </summary>
        /// <param name="employee">employee to delete</param>
        Task DeleteEmployee(Employee employee);

        /// <summary>
        /// Search Employees by name, department, or email
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>List of Employee</returns>
        Task<List<Employee>> SearchEmployees(string text);
    }
}