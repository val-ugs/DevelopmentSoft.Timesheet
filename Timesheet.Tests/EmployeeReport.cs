using System;
using System.Collections.Generic;
using Timesheet.Api.Models;

namespace Timesheet.Tests
{
    partial class ReportServiceTests
    {
        public class EmployeeReport
        {
            public string LastName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            public List<TimeLog> TimeLogs { get; set; }

            public int TotalHours { get; set; }
            public int Bill { get; set; }
        }
    }
}
