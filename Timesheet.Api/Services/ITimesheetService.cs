using Timesheet.Api.Models;

namespace Timesheet.Api.Services
{
    public interface ITimesheetService
    {
        bool TrackTime(TimeLog timelog);
    }
}