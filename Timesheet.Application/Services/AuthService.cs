﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Application.Services
{
    public class AuthService : IAuthService
    {
        private IEmployeeRepository _employeeRepository;

        public AuthService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool Login(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return false;
            }

            StaffEmployee staffEmployee = _employeeRepository.GetEmployee(lastName);
            var isEmployeeExist = staffEmployee != null;

            if (isEmployeeExist)
                UserSession.Sessions.Add(lastName);

            return isEmployeeExist;
        }

        public static class UserSession
        {

            public static HashSet<string> Sessions { get; set; } = new HashSet<string>();
        }
    }
}
