using EMS_Api.CreateEmployeeAggreagate;
using FluentValidation.Results;

namespace EMS_UnitTests.EmployeeAggreagate
{
    /// <summary>
    /// Class <see cref="EmployeeValidatorUnitTest"/> containing unit tests.
    /// </summary>
    public class EmployeeValidatorUnitTest
    {
        /// <summary>
        /// EmployeeValidator With Correct datas should returns True.
        /// </summary>
        [Fact]
        public async Task EmployeeValidator_WithCorrectdatas_returnsTrue()
        {
            // Arrange
            EmployeeValidator employeeValidator = new EmployeeValidator();

            EmployeeRequest employeeRequest = new()
            {
                DateOfBirth = DateTime.Now.AddDays(-50),
                Department = "Department",
                Email = "test@test.com",
                Name = "Name"
            };

            // Act
            ValidationResult result = await employeeValidator.ValidateAsync(employeeRequest);

            // Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// EmployeeValidator With Invalid date of birth should returns False.
        /// </summary>
        [Fact]
        public async Task EmployeeValidator_WithInvalidDateOfBirth_returnsFalse()
        {
            // Arrange
            EmployeeValidator employeeValidator = new EmployeeValidator();

            EmployeeRequest employeeRequest = new()
            {
                DateOfBirth = DateTime.Now.AddDays(50),
                Department = "department",
                Email = "test@test.te",
                Name = "name"
            };

            // Act
            ValidationResult result = await employeeValidator.ValidateAsync(employeeRequest);

            // Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// EmployeeValidator With Invalid email should returns False.
        /// </summary>
        [Fact]
        public async Task EmployeeValidator_WithInvalidEmail_returnsFalse()
        {
            // Arrange
            EmployeeValidator employeeValidator = new EmployeeValidator();

            EmployeeRequest employeeRequest = new()
            {
                DateOfBirth = DateTime.Now.AddDays(-50),
                Department = "department",
                Email = "test",
                Name = "name"
            };

            // Act
            ValidationResult result = await employeeValidator.ValidateAsync(employeeRequest);

            // Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// EmployeeValidator With empty datas should returns False.
        /// </summary>
        [Theory]
        [InlineData("", "test@test.fr", "" )]
        [InlineData("department", "test@test.fr", "" )]
        [InlineData("department", "", "" )]
        [InlineData("", "test@test.fr", "name" )]
        [InlineData("", "", "name" )]
        public async Task EmployeeValidator_WithEmptydatas_returnsFalse(string department, string email, string name)
        {
            // Arrange
            EmployeeValidator employeeValidator = new EmployeeValidator();

            EmployeeRequest employeeRequest = new()
            {
                DateOfBirth = DateTime.Now.AddDays(-50),
                Department = department,
                Email = email,
                Name = name
            };

            // Act
            ValidationResult result = await employeeValidator.ValidateAsync(employeeRequest);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}