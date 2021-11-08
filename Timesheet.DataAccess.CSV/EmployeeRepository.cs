using System;
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
        private const char DELIMETER = ';';
        private const string PATH = "..\\Timesheet.DataAccess.CSV\\Data\\employees.csv";

        public void AddEmployee(StaffEmployee staffEmployee)
        {
            var dataRow = $"{staffEmployee.LastName}{DELIMETER}" +
                $"{staffEmployee.Salary}\n";
            File.AppendAllText(PATH, dataRow);
        }

        public StaffEmployee GetEmployee(string lastName)
        {
            var data = File.ReadAllText(PATH);
            var dataRows = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StaffEmployee staffEmployee = null;

            foreach (var dataRow in dataRows)
            {
                if (dataRow.Contains(lastName))
                {
                    var dataMembers = dataRow.Split(DELIMETER);
                    staffEmployee = new StaffEmployee()
                    {
                        LastName = dataMembers[0],
                        Salary = decimal.TryParse(dataMembers[1], out var salary) ? salary : 0
                    }
                    break;
                }
            }
            return staffEmployee;
        }
    }
}
