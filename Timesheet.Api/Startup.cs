using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.Api.ResourceModels;
using Timesheet.BussinessLogic.Services;
//using Timesheet.DataAccess.CSV;
using Timesheet.DataAccess.MSSQL;
using Timesheet.DataAccess.MSSQL.Repositories;
using Timesheet.Domain;
using Timesheet.Integrations.GitHub;

namespace Timesheet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DataAccessMappingProfile));

            services.AddTransient<IValidator<CreateTimeLogRequest>, TimeLogFluentValidator>();
            services.AddTransient<IValidator<LoginRequest>, LoginRequestFluentValidator>();
            
            //services.AddTransient<ITimesheetRepository, DataAccess.CSV.TimesheetRepository>();
            services.AddTransient<ITimesheetRepository, TimesheetRepository>();
            //services.AddTransient<IEmployeeRepository, DataAccess.CSV.EmployeeRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITimesheetService, TimesheetService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IIssuesService, IssuesService>();

            services.AddTransient<IIssuesClient>(x => new IssuesClient("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"));

            //services.AddSingleton(x => new CsvSettings(';', "..\\Timesheet.DataAccess.CSV\\Data"));

            services.AddOptions<JwtConfig>()
                .Bind(Configuration.GetSection("JwtConfig"));

            services.AddDbContext<TimesheetContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("TimesheetContext")));

            services.AddControllers().AddFluentValidation();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtAuthMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
