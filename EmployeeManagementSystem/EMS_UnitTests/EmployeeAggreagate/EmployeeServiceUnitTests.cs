using AutoMapper;
using EMS_Domain.EmployeeAggreagate;
using EMS_Service.EmployeeAggreagate;
using Moq;

namespace EMS_UnitTests.EmployeeAggreagate
{
    /// <summary>
    /// Class <see cref="EmployeeServiceUnitTests"/> containing unit tests of service  <see cref="EmployeeService"/>.
    /// </summary>
    public class EmployeeServiceUnitTests
    {
        public readonly EmployeeService _employeeService;
        public readonly Mock<IEmployeeRepository> _mockEmployeeRepository;

        /// <summary>
        /// Constructor of EmployeeServiceUnitTests.
        /// </summary>
        public EmployeeServiceUnitTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            EmployeeProfile employeeProfile = new EmployeeProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(employeeProfile));
            IMapper mapper = new Mapper(configuration);
            _employeeService = new(_mockEmployeeRepository.Object, mapper);
        }

        /// <summary>
        /// Create Employee Ok.
        /// </summary>
        [Fact]
        public async Task CreateEmployee_Ok()
        {
            // Arrange
            EmployeeDto employeeToCreate = new EmployeeDto()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                Department = "department",
                Email = "Email",
                Name = "name"
            };

            _mockEmployeeRepository.Setup(x => x.CreateEmployee(It.IsAny<Employee>())).ReturnsAsync(new Employee());

            // Act
            EmployeeDto employeeCreated = await _employeeService.CreateEmployee(employeeToCreate);

            // Assert
            Assert.NotNull(employeeCreated);
        }

        /// <summary>
        /// Update Employee Ok.
        /// </summary>
        [Fact]
        public async Task UpdateEmployee_Ok()
        {
            // Arrange
            EmployeeDto employeeToUpdate = new EmployeeDto()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                Department = "department",
                Email = "Email",
                Name = "name",
                Id = 2
            };

            _mockEmployeeRepository.Setup(x => x.UpdateEmployee(It.IsAny<Employee>()));

            // Act
            await _employeeService.CreateEmployee(employeeToUpdate);

            // Assert
            Assert.True(true);
        }

        /// <summary>
        /// Delete Employee Ok.
        /// </summary>
        [Fact]
        public async Task DeleteEmployee_Ok()
        {
            // Arrange
            EmployeeDto employeeToDelete = new EmployeeDto()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                Department = "department",
                Email = "Email",
                Name = "name",
                Id = 2
            };

            _mockEmployeeRepository.Setup(x => x.DeleteEmployee(It.IsAny<Employee>()));

            // Act
            await _employeeService.DeleteEmployee(employeeToDelete);

            // Assert
            Assert.True(true);
        }

        /// <summary>
        /// Get Employee by id Ok.
        /// </summary>
        [Fact]
        public async Task GetEmployeeById_Ok()
        {
            // Arrange
            int idEmployee = 5;

            _mockEmployeeRepository.Setup(x => x.GetEmployee(It.IsAny<int>())).ReturnsAsync(new Employee());

            // Act
            EmployeeDto employeeDto= await _employeeService.GetEmployee(idEmployee);

            // Assert
            Assert.NotNull(employeeDto);
        }

        /// <summary>
        /// Get All Employees Ok.
        /// </summary>
        [Fact]
        public async Task GetAllEmployees_Ok()
        {
            // Arrange
            _mockEmployeeRepository.Setup(x => x.GetAllEmployees()).ReturnsAsync(new List<Employee>()
            {
                new Employee() {},
                new Employee() {},
            });

            // Act
            List<EmployeeDto> allEmployees = await _employeeService.GetAllEmployees();

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(2,allEmployees.Count);
        }

        /// <summary>
        /// Search Employees Ok.
        /// </summary>
        [Fact]
        public async Task SearchEmployees_Ok()
        {
            // Arrange
            string text = "tes";

            _mockEmployeeRepository.Setup(x => x.SearchEmployees(It.IsAny<string>())).ReturnsAsync(new List<Employee>()
            {
                new Employee() {},
                new Employee() {},
                new Employee() {},
            });

            // Act
            List<EmployeeDto> allEmployees = await _employeeService.SearchEmployees(text);

            // Assert
            Assert.NotNull(allEmployees);
            Assert.Equal(3, allEmployees.Count);
        }
    }
}