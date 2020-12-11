using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recruitment.DataAccess.Extensions;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.DataAccess.Configurations
{
    public class QuestionnaireResultConfiguration : IEntityTypeConfiguration<QuestionnaireResult>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireResult> builder)
        {
            builder.Property(result => result.Answers)
                .HasJsonConversion();
        }
    }
}
