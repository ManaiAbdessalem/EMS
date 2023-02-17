using AutoMapper;
using EMS_Api.Controllers;
using EMS_Api.CreateEmployeeAggreagate;
using EMS_Api.EmployeeAggreagate;
using EMS_Service.EmployeeAggreagate;
using Microsoft.AspNetCore.Mvc;
using Moq;
using EmployeeProfile = EMS_Api.CreateEmployeeAggreagate.EmployeeProfile;

namespace EMS_UnitTests.EmployeeAggreagate
{
    /// <summary>
    /// Class <see cref="EmployeeControllerUnitTests"/> containing unit tests of controller  <see cref="EmployeeController"/>.
    /// </summary>
    public class EmployeeControllerUnitTests
    {
        public readonly EmployeeController _employeeController;
        public readonly Mock<IEmployeeService> _mockEmployeeService;

        /// <summary>
        /// Constructor of EmployeeControllerUnitTests.
        /// </summary>
        public EmployeeControllerUnitTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            EmployeeProfile employeeProfile = new EmployeeProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(employeeProfile));
            IMapper mapper = new Mapper(configuration);
            _employeeController = new(_mockEmployeeService.Object, mapper);
        }

        /// <summary>
        /// Create Employee Ok.
        /// </summary>
        [Fact]
        public async Task CreateEmployee_Ok()
        {
            // Arrange
            EmployeeRequest employeeToCreate = new EmployeeRequest()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                Department = "department",
                Email = "Email",
                Name = "name"
            };

            _mockEmployeeService.Setup(x => x.CreateEmployee(It.IsAny<EmployeeDto>())).ReturnsAsync(new EmployeeDto());

            // Act
            ActionResult<EmployeeResponse> employeeCreated = await _employeeController.CreateEmployee(employeeToCreate);

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
            int idEmployeeToUpdate = 2;
            UpdateEmployeeRequest employeeToUpdate = new UpdateEmployeeRequest()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                Department = "department",
                Email = "Email",
                Name = "name",
                Id = idEmployeeToUpdate
            };

            _mockEmployeeService.Setup(x => x.UpdateEmployee(It.IsAny<EmployeeDto>()));
            _mockEmployeeService.Setup(x => x.GetEmployee(It.IsAny<int>())).ReturnsAsync(new EmployeeDto());

            // Act
            ActionResult employeeToUpdateResult = await _employeeController.UpdateEmployee(idEmployeeToUpdate, employeeToUpdate);

            // Assert
            Assert.IsType<NoContentResult>(employeeToUpdateResult);
        }

        /// <summary>
        /// Update Employee Return BadRequest when ids are not equals.
        /// </summary>
        [Fact]
        public async Task UpdateEmployee_IdsNotEquals_ReturnBadRequest()
        {
            // Arrange
            int idEmployeeToUpdate = 2;
            UpdateEmployeeRequest employeeToUpdate = new UpdateEmployeeRequest()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                Department = "department",
                Email = "Email",
                Name = "name",
                Id = 4
            };

            _mockEmployeeService.Setup(x => x.UpdateEmployee(It.IsAny<EmployeeDto>()));
            _mockEmployeeService.Setup(x => x.GetEmployee(It.IsAny<int>())).ReturnsAsync(new EmployeeDto());

            // Act
            ActionResult employeeToUpdateResult = await _employeeController.UpdateEmployee(idEmployeeToUpdate, employeeToUpdate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(employeeToUpdateResult);
        }

        /// <summary>
        /// Update Employee Return NotFound when employee id not exists.
        /// </summary>
        [Fact]
        public async Task UpdateEmployee_EmployeeNotFound_ReturnNotFound()
        {
            // Arrange
            int idEmployeeToUpdate = 2;
            UpdateEmployeeRequest employeeToUpdate = new UpdateEmployeeRequest()
            {
                DateOfBirth = DateTime.Now.AddYears(-20),
                Department = "department",
                Email = "Email",
                Name = "name",
                Id = idEmployeeToUpdate
            };

            _mockEmployeeService.Setup(x => x.UpdateEmployee(It.IsAny<EmployeeDto>()));
            _mockEmployeeService.Setup(x => x.GetEmployee(It.IsAny<int>()));

            // Act
            ActionResult employeeToUpdateResult = await _employeeController.UpdateEmployee(idEmployeeToUpdate, employeeToUpdate);

            // Assert
            Assert.IsType<NotFoundObjectResult>(employeeToUpdateResult);
        }

        /// <summary>
        /// Delete Employee Ok.
        /// </summary>
        [Fact]
        public async Task DeleteEmployee_Ok()
        {
            // Arrange
            int idEmployeeToDelete = 2;

            _mockEmployeeService.Setup(x => x.DeleteEmployee(It.IsAny<EmployeeDto>()));
            _mockEmployeeService.Setup(x => x.GetEmployee(It.IsAny<int>())).ReturnsAsync(new EmployeeDto());

            // Act
            ActionResult employeeToDeleteResult = await _employeeController.DeleteEmployee(idEmployeeToDelete);

            // Assert
            Assert.IsType<NoContentResult>(employeeToDeleteResult);
        }

        /// <summary>
        /// Delete Employee Return NotFound when employee id not exists.
        /// </summary>
        [Fact]
        public async Task DeleteEmployee_EmployeeNotFound_ReturnNotFound()
        {
            // Arrange
            int idEmployeeToDelete = 2;

            _mockEmployeeService.Setup(x => x.DeleteEmployee(It.IsAny<EmployeeDto>()));
            _mockEmployeeService.Setup(x => x.GetEmployee(It.IsAny<int>()));

            // Act
            ActionResult employeeToDeleteResult = await _employeeController.DeleteEmployee(idEmployeeToDelete);

            // Assert
            Assert.IsType<NotFoundObjectResult>(employeeToDeleteResult);
        }

        /// <summary>
        /// Get all employees returns all employees.
        /// </summary>
        [Fact]
        public async Task GetAllEmployees_Ok()
        {
            // Arrange

            _mockEmployeeService.Setup(x => x.GetAllEmployees()).ReturnsAsync(new List<EmployeeDto>
            {
                new EmployeeDto { Id = 1},
                new EmployeeDto { Id = 2 },
                new EmployeeDto { Id = 3 }
            });
            _mockEmployeeService.Setup(x => x.GetEmployee(It.IsAny<int>()));

            // Act
            ActionResult<List<EmployeeResponse>> employeeToDeleteResult = await _employeeController.GetAll();

            // Assert
            Assert.Equal(3, employeeToDeleteResult.Value?.Count);
        }

        /// <summary>
        /// Get employee by id.
        /// </summary>
        [Fact]
        public async Task GetEmployeeById_Ok()
        {
            // Arrange
            _mockEmployeeService.Setup(x => x.GetEmployee(It.IsAny<int>())).ReturnsAsync(new EmployeeDto());

            // Act
            ActionResult<EmployeeResponse> employee = await _employeeController.GetEmployeeById(1);

            // Assert
            Assert.NotNull(employee);
        }

        /// <summary>
        /// Search employees ok.
        /// </summary>
        [Fact]
        public async Task SearchEmployees_Ok()
        {
            // Arrange

            string text = "test";
            _mockEmployeeService.Setup(x => x.SearchEmployees(It.IsAny<string>())).ReturnsAsync(new List<EmployeeDto>
            {
                new EmployeeDto { Id = 1},
                new EmployeeDto { Id = 2 }
            });

            // Act
            ActionResult<List<EmployeeResponse>> employeeToDeleteResult = await _employeeController.SearchEmployees(text);

            // Assert
            Assert.Equal(2, employeeToDeleteResult.Value?.Count);
        }
    }
}