using AutoMapper;
using EMS_Api.CreateEmployeeAggreagate;
using EMS_Api.EmployeeAggreagate;
using EMS_Service.EmployeeAggreagate;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS_Api.Controllers
{
    /// <summary>
    /// Controller <see cref="EmployeeController"/> containing Employee actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IMapper _mapper { get; set; }
        private IEmployeeService _employeeService { get; set; }

        /// <summary>
        /// Constructor of EmployeeController.
        /// </summary>
        /// <param name="employeeService">employeeService</param>
        /// <param name="mapper">mapper</param>
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all employees.
        /// </summary>
        /// <returns>List of EmployeeResponse</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<List<EmployeeResponse>>> GetAll()
        {
            List<EmployeeDto> employeesDto = await _employeeService.GetAllEmployees();
            return _mapper.Map<List<EmployeeResponse>>(employeesDto);
        }

        /// <summary>
        /// Get employee by id.
        /// </summary>
        /// <returns>EmployeeResponse</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById(int id)
        {
            EmployeeDto employeeDto = await _employeeService.GetEmployee(id);
            if (employeeDto is null)
            {
                return NotFound("Employee does not exists!");
            }

            return _mapper.Map<EmployeeResponse>(employeeDto);
        }

        /// <summary>
        /// Search Employees by name, department, or email.
        /// </summary>
        /// <returns>List of EmployeeResponse</returns>n
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("search/{text}")]
        public async Task<ActionResult<List<EmployeeResponse>>> SearchEmployees(string text)
        {
            if (string.IsNullOrEmpty(text?.Trim()))
            {
                return BadRequest("keyword is empty!");
            }
            List<EmployeeDto> employeesDto = await _employeeService.SearchEmployees(text);
            return _mapper.Map<List<EmployeeResponse>>(employeesDto);
        }

        /// <summary>
        /// Create employee.
        /// </summary>
        /// <param name="createEmployeeRequest">createEmployeeRequest</param>
        /// <returns>EmployeeResponse</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> CreateEmployee([FromBody] EmployeeRequest createEmployeeRequest)
        {
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(createEmployeeRequest);
            employeeDto = await _employeeService.CreateEmployee(employeeDto);
            return _mapper.Map<EmployeeResponse>(employeeDto);
        }

        /// <summary>
        /// Update an Employee.
        /// </summary>
        /// <param name="id">id of Employee to update</param>
        /// <param name="updateEmployeeRequest">updateEmployeeRequest</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, UpdateEmployeeRequest updateEmployeeRequest)
        {
            if (id != updateEmployeeRequest.Id)
            {
                return BadRequest("Ids does not match!");
            }

            EmployeeDto employeeToUpdate = await _employeeService.GetEmployee(id);

            if (employeeToUpdate is null)
            {
                return NotFound("Employee to update does not exists!");
            }

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(updateEmployeeRequest);

            await _employeeService.UpdateEmployee(employeeDto);
            return NoContent();
        }

        /// <summary>
        /// Delte an employee.
        /// </summary>
        /// <param name="id">id employee to delete</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            EmployeeDto employeeToDelete = await _employeeService.GetEmployee(id);

            if (employeeToDelete is null)
            {
                return NotFound("Employee to delete does not exists!");
            }

            await _employeeService.DeleteEmployee(employeeToDelete);
            return NoContent();
        }


        /// <summary>
        /// Throw exception --> used to demonstrate Azure Appinsights.
        /// </summary>
        [HttpGet("exception")]
        public async Task<ActionResult<List<EmployeeResponse>>> ThrowAxception()
        {
            throw new Exception("Exception used to demonstrate Azure Appinsights");
        }
    }
}