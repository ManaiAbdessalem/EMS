using FluentValidation;

namespace EMS_Api.CreateEmployeeAggreagate
{
    /// <summary>
    /// EmployeeValidator Validator
    /// </summary>
    public class EmployeeValidator : AbstractValidator<EmployeeRequest>
    {
        /// <summary>
        /// Constructor of CreateEmployeeValidator.
        /// </summary>
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Department).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now);
        }
    }
}