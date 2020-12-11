using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Recruitment.Api.Services;
using Recruitment.BusinessLogic.Services;
using Recruitment.BusinessLogic.Services.Interfaces;
using Recruitment.BusinessLogic.Validators;

namespace Recruitment.Api.Extensions
{
    internal static class StartupExtensions
    {
        public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JwtIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JwtAudience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, JwtAuthenticationService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IRecruiterService, RecruiterService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<IQuestionnaireService, QuestionnaireService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<QuestionnaireValidator>();
        }
    }
}
