using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Models;

namespace Timesheet.Domain
{
    public interface IIssuesService
    {
        Issues[] Get(string expectedLogin, string expectedProject);
    }
}
