using HADI.Models;
using System.Collections.Generic;

namespace HADI.Repository.IRepository
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
