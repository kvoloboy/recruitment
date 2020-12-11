using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recruitment.DataAccess.Context;
using Recruitment.Domain.Models.Entities;
using Recruitment.Api.Extensions;
using Recruitment.BusinessLogic.Profiles;

namespace Recruitment.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<RecruitmentDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RecruitmentDbContext")));

            services.AddAutoMapper(cfg =>
            {
                var profiles = new Profile[]
                {
                    new RecruiterProfile(),
                    new CandidateProfile(),
                    new QuestionnaireProfile(),
                    new VacancyProfile()
                };

                cfg.AddProfiles(profiles);
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RecruitmentDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtAuth(Configuration);

            services.AddBusinessLogicServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
