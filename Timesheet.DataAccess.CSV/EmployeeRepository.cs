﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.CSV
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly char _delimeter;
        private readonly string _path;

        public EmployeeRepository(CsvSettings csvSettings)
        {
            _delimeter = csvSettings.Delimeter;
            _path = csvSettings.Path + "\\employees.csv";
        }

        public void Add(Employee employee)
        {
            var dataRow = $"{employee.LastName}{_delimeter}" +
                $"{employee.Salary}{_delimeter}" + $"{employee.Position}\n";
            File.AppendAllText(_path, dataRow);
        }

        public Employee Get(string lastName)
        {
            var data = File.ReadAllText(_path);
            var dataRows = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Employee employee = null;

            foreach (var dataRow in dataRows)
            {
                if (dataRow.Contains(lastName))
                {
                    var dataMembers = dataRow.Split(_delimeter);
                    decimal salary = 0;
                    decimal.TryParse(dataMembers[1], out salary);
                    var position = dataMembers[2];
                    switch (position)
                    {
                        case "Руководитель":
                            decimal bonus = 0;
                            decimal.TryParse(dataMembers[1], out bonus);
                            employee = new ChiefEmployee(lastName, salary, bonus);
                            break;
                        case "Штатный сотрудник":
                            employee = new StaffEmployee(lastName, salary);
                            break;
                        case "Фрилансер":
                            employee = new FreelancerEmployee(lastName, salary);
                            break;
                        default:
                            break;
                    }break;
                }
            }
            return employee;
        }
    }
}
