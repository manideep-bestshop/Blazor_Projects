using System.ComponentModel.DataAnnotations;

namespace Employee.API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }

        [Range(1, 1000000)]
        public decimal Salary { get; set; }
    }
}
