using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.DataAccess.Configurations
{
    public class VacancyApplicationConfiguration : IEntityTypeConfiguration<VacancyApplication>
    {
        public void Configure(EntityTypeBuilder<VacancyApplication> builder)
        {
            builder.HasKey(x => new { x.CandidateId, x.VacancyId });

            builder.HasOne(x => x.Candidate)
                .WithMany(x => x.VacancyApplications)
                .HasForeignKey(x => x.CandidateId);

            builder.HasOne(x => x.Vacancy)
                .WithMany(x => x.VacancyApplications)
                .HasForeignKey(x => x.VacancyId);
        }
    }
}
