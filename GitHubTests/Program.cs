using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubTests
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));

            var tokenAuth = new Credentials("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"); // NOTE: not real token
            client.Credentials = tokenAuth;

            var user = await client.User.Get("val-ugs");
            Console.WriteLine("{0} has {1} public repositories - go check out their profile at {2}",
                user.Name,
                user.PublicRepos,
                user.Url);

            var projects = await client
                .Repository
                .Project
                .GetAllForRepository("val-ugs", "DevelopmentSoft.Timesheet");

            var timesheetProject = projects.FirstOrDefault();

            var columns = await client.Repository.Project.Column
                .GetAll(timesheetProject.Id);

            var cards = new List<ProjectCard>();

            foreach (var column in columns)
            {
                var columnCards = await client.Repository.Project.Card
                    .GetAll(column.Id);

                cards.AddRange(columnCards);
            }
        }
    }
}
