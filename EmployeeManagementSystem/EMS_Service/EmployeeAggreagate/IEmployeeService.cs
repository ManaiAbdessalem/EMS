using EMS_Domain.EmployeeAggreagate;

namespace EMS_Service.EmployeeAggreagate
{
    /// <summary>
    /// Interface <see cref="IEmployeeService"/> containing Employee services.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Creates an Employee.
        /// </summary>
        /// <param name="employeeDto">employeeDto input</param>
        /// <returns>employee</returns>
        Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto);

        /// <summary>
        /// Updates an Employee.
        /// </summary>
        /// <param name="employeeDto">employeeDto input</param>
        Task UpdateEmployee(EmployeeDto employeeDto);

        /// <summary>
        /// Get Employee by id.
        /// </summary>
        /// <param name="id">id employee</param>
        /// <returns>EmployeeDto</returns>
        Task<EmployeeDto> GetEmployee(int id);

        /// <summary>
        /// Return all employees.
        /// </summary>
        /// <returns>List of Employee</returns>
        Task<List<EmployeeDto>> GetAllEmployees();

        /// <summary>
        /// Delete an Employee.
        /// </summary>
        /// <param name="employeeDto">employee to delete</param>
        Task DeleteEmployee(EmployeeDto employeeDto);


        /// <summary>
        /// Search Employees by name, department, or email
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>List of Employee</returns>
        Task<List<EmployeeDto>> SearchEmployees(string text);

    }
}