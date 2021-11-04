using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.Api.Services;
using static Timesheet.Api.Services.AuthService;

namespace Timesheet.Tests
{
    class TimesheetServiceTests
    {
        [Test]
        public void TrackTime_ShouldReturnTrue()
        {
            // arrange
            var expectedLastName = "TestUser";

            UserSession.Sessions.Add(expectedLastName);

            var timelog = new TimeLog
            {
                Date = new DateTime(),
                workingHours = 1,
                LastName = expectedLastName,
                Comment = Guid.NewGuid().ToString()
            };

            var service = new TimesheetService();

            // act
            var result = service.TrackTime(timelog);

            // assert
            Assert.IsTrue(result);
        }

        [TestCase(25, "")]
        [TestCase(25, null)]
        [TestCase(25, "TestUser")]
        [TestCase(-1, "")]
        [TestCase(-1, null)]
        [TestCase(-1, "TestUser")]
        [TestCase(4, "")]
        [TestCase(4, null)]
        [TestCase(4, "TestUser")]
        public void TrackTime_ShouldReturnFalse(int hours, string lastName)
        {
            // arrange

            var timelog = new TimeLog
            {
                Date = new DateTime(),
                workingHours = hours,
                LastName = lastName,
                Comment = Guid.NewGuid().ToString()
            };

            var service = new TimesheetService();

            // act
            var result = service.TrackTime(timelog);

            // assert
            Assert.IsFalse(result);
        }
    }
}
