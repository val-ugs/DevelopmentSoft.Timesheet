using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Models
{
    public class FreelancerEmployee : Employee
    {
        public FreelancerEmployee(string lastname, decimal salary) : base(lastname, salary, Position.Freelancer)
        {
        }

        public override decimal CalculateBill(TimeLog[] timeLogs)
        {
            var totalHours = timeLogs.Sum(x => x.WorkingHours);
            decimal bill = totalHours * Salary;

            return bill;
        }

        public override string GetPersonalData(char delimeter)
        {
            return $"{LastName}{delimeter}{Salary}{delimeter}Фрилансер{delimeter}\n";
        }

        public override bool CheckInputLog(TimeLog timeLog)
        {
            bool isValid = base.CheckInputLog(timeLog);
            isValid = timeLog.Name == this.LastName && timeLog.Date > DateTime.Now.AddDays(-2) && isValid;
            return isValid;
        }
    }
}
