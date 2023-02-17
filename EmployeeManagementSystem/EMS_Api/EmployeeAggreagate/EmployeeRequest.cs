namespace EMS_Api.CreateEmployeeAggreagate
{
    /// <summary>
    /// Class <see cref="EmployeeRequest"/>
    /// </summary>
    public class EmployeeRequest
    {
        /// <summary>
        /// Employee Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Employee date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Employee Department.
        /// </summary>
        public string Department { get; set; }
    }
}