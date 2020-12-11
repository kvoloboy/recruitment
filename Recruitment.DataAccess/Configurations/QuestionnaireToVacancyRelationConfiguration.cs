using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.DataAccess.Configurations
{
    public class QuestionnaireToVacancyRelationConfiguration : IEntityTypeConfiguration<QuestionnaireToVacancyRelation>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireToVacancyRelation> builder)
        {
            builder.HasKey(x => new { x.VacancyId, x.QuestionnaireId });

            builder.HasOne(x => x.Questionnaire)
                .WithMany(x => x.QuestionnaireToVacancyRelations)
                .HasForeignKey(x => x.QuestionnaireId);

            builder.HasOne(x => x.Vacancy)
                .WithMany(x => x.QuestionnaireToVacancyRelations)
                .HasForeignKey(x => x.VacancyId);
        }
    }
}
