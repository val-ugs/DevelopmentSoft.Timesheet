using NUnit.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Api.Services;

namespace Timesheet.Tests
{
    partial class ReportServiceTests
    {
        [Test]
        public void GetEmployeeReport_ShouldReturnReport()
        {
            // arrange

            var service = new ReportService();

            var expectedLastName = "Иванов";
            var expectedTotal = 100;

            // act
            var result = service.GetEmployeeReport(expectedLastName);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
        }
    }
}
