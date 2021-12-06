﻿using HADI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HADI.Models
{
    public class SQlEmployeeRep : IEmployeeRepo
    {
        private readonly AppDbContext _context;

        public SQlEmployeeRep(AppDbContext context)
        {
            _context = context;
        }

        public Employee AddEmployee(Employee newEmployee)
        {
            _context.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id); 

            if(employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            return employee;
          
        }

        public Employee UpdateEmployee(Employee employeeChange)
        {
            _context.Employees.Attach(employeeChange);
            _context.SaveChanges();
            return employeeChange;
        }
    }
}
