using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Api.Services
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {
            Employees = new List<string>
            {
                "Иванов",
                "Петров",
                "Сидоров"
            };
        }

        public List<string> Employees { get; private set; }

        public bool Login(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return false;
            }

            var isEmployeeExist = Employees.Contains(lastName);

            if (isEmployeeExist)
                UserSession.Sessions.Add(lastName);

            return Employees.Contains(lastName);
        }

        public static class UserSession
        {

            public static HashSet<string> Sessions { get; set; } = new HashSet<string>();
        }
    }
}
