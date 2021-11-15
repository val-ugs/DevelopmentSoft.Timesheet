﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.BussinessLogic.Services;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : Controller
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpPost]
        public ActionResult<bool> TrackTime(CreateTimeLogRequest request)
        {
            var lastname = (string)HttpContext.Items["LastName"];

            if (ModelState.IsValid)
            {
                var timeLog = new TimeLog
                {
                    Comment = request.Comment,
                    Date = request.Date,
                    LastName = request.LastName,
                    WorkingHours = request.WorkingHours
                };

                var result = _timesheetService.TrackTime(timeLog, lastname);
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
