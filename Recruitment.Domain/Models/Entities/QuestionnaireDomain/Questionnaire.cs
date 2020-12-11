using System.Collections.Generic;

namespace Recruitment.Domain.Models.Entities.QuestionnaireDomain
{
    public class Questionnaire : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<QuestionnaireToVacancyRelation> QuestionnaireToVacancyRelations { get; set; }
    }
}
