using AutoMapper;
using EMS_Domain.EmployeeAggreagate;

namespace EMS_Service.EmployeeAggreagate
{
    /// <summary>
    /// Class <see cref="EmployeeService"/> containing Employee Services.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository { get; set; }
        private IMapper _mapper { get; set; }

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.CreateEmployee(employee);
            return _mapper.Map<EmployeeDto>(employee);
        }

        /// <inheritdoc />
        public async Task<EmployeeDto> GetEmployee(int id)
        {
            Employee employee = await _employeeRepository.GetEmployee(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        /// <inheritdoc />
        public async Task UpdateEmployee(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.UpdateEmployee(employee);
        }

        /// <inheritdoc />
        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            List<Employee> employees = await _employeeRepository.GetAllEmployees();
            List<EmployeeDto> employeesDto = _mapper.Map<List<EmployeeDto>>(employees);
            return employeesDto;
        }

        /// <inheritdoc />
        public async Task DeleteEmployee(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.DeleteEmployee(employee);
        }

        /// <inheritdoc />
        public async Task<List<EmployeeDto>> SearchEmployees(string text)
        {
            List<Employee> employeesResult = await _employeeRepository.SearchEmployees(text);
            List<EmployeeDto> employeesResultDto = _mapper.Map<List<EmployeeDto>>(employeesResult);
            return employeesResultDto;
        }
    }
}