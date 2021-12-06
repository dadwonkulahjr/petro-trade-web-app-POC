using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public interface IEmployeeRepo
    {
        Employee GetEmployee(int id);
        Employee AddEmployee(Employee newEmployee);
        IEnumerable<Employee> GetAllEmployee();
        Employee DeleteEmployee(int id);
        Employee UpdateEmployee(Employee employeeChange);
    }
}
