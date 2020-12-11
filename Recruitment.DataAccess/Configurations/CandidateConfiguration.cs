using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recruitment.Domain.Models.Entities;
using Recruitment.DataAccess.Extensions;

namespace Recruitment.DataAccess.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.Property(candidate => candidate.Skills)
                .HasJsonConversion();
        }
    }
}
