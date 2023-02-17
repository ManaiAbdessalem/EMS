namespace EMS_Api.EmployeeAggreagate
{
    /// <summary>
    /// Class <see cref="EmployeeResponse"/>
    /// </summary>
    public class  EmployeeResponse
    {
        /// <summary>
        /// Id of Employee.
        /// </summary>
        public int Id { get; set; }

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
