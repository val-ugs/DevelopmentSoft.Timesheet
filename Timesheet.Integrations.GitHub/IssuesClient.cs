using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Integrations.GitHub
{
    public class IssuesClient : Domain.IIssuesClient
    {
        private readonly IGitHubClient _gitHubClient;
        
        public IssuesClient(string token)
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app")); // NOTE: not real token
            var tokenAuth = new Credentials(token);
            client.Credentials = tokenAuth;

            _gitHubClient = client;
        }

        public async Task<Issues[]> Get(string managerLogin, string project)
        {
            var projects = await _gitHubClient
                .Repository
                .Project
                .GetAllForRepository(managerLogin, "project name");

            var timesheetProject = projects.FirstOrDefault();

            var columns = await _gitHubClient.Repository.Project.Column
                .GetAll(timesheetProject.Id);

            var issues = new List<Issues>();

            foreach (var column in columns)
            {
                var columnCards = await _gitHubClient.Repository.Project.Card
                    .GetAll(column.Id);
                
                var columnIssues = columnCards.Select(x => new Issues
                {
                    Id = 0,
                    Name = x.Note ?? x.ContentUrl,
                    SourceId = x.Id
                });

                issues.AddRange(columnIssues);
            }

            return issues.ToArray();
        }
    }
}
