using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name Cannot exceed twenty (20) characters")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public string Phone  { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public int Salary { get; set; }
        public Assignment AssignTo { get; set; }
        public string SSID { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public DateTime EmployementDate { get; set; }
        public string EmploymentStatus { get; set; }
        public string Supervisor { get; set; }
        public string PhotoPath { get; set; }
    }
}
