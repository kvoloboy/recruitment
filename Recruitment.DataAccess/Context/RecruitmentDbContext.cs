using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recruitment.DataAccess.Configurations;
using Recruitment.Domain.Models.Entities;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.DataAccess.Context
{
    public sealed class RecruitmentDbContext : IdentityDbContext<User>
    {
        public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Vacancy> Vacancies { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Recruiter> Recruiters { get; set; }

        public DbSet<QuestionnaireResult> QuestionnaireResults { get; set; }

        public DbSet<Questionnaire> Questionnaires { get; set; }

        public DbSet<QuestionnaireToVacancyRelation> QuestionnaireToVacancyRelations { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<VacancyApplication> VacancyApplications { get; set; }

        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new QuestionnaireResultConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionnaireToVacancyRelationConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyConfiguration());
            modelBuilder.ApplyConfiguration(new CandidateConfiguration());
        }
    }
}
