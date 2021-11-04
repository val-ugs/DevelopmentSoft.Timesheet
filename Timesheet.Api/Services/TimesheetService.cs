using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using static Timesheet.Api.Services.AuthService;

namespace Timesheet.Api.Services
{
    public class TimesheetService
    {
        public bool TrackTime(TimeLog timelog)
        {
            bool isValid = timelog.workingHours > 0 && timelog.workingHours <= 24 && !string.IsNullOrWhiteSpace(timelog.LastName);

            isValid = isValid && UserSession.Sessions.Contains(timelog.LastName);

            if (!isValid)
            {
                return false;
            }
            
            Timesheets.TimeLogs.Add(timelog);

            return true;
        }
    }

    public static class Timesheets
    {

        public static List<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
    }
}
