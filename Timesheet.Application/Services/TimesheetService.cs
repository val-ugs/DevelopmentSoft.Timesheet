using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Domain;
using Timesheet.Domain.Models;
using static Timesheet.Application.Services.AuthService;

namespace Timesheet.Application.Services
{
    public class TimesheetService : ITimesheetService
    {
        public bool TrackTime(TimeLog timelog)
        {
            bool isValid = timelog.WorkingHours > 0 && timelog.WorkingHours <= 24 && !string.IsNullOrWhiteSpace(timelog.LastName);

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
