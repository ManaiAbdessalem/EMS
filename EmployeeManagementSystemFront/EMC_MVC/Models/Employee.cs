namespace EMC_MVC.Models
{
    /// <summary>
    /// Class <see cref="Employee"/>
    /// </summary>
    public class Employee
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