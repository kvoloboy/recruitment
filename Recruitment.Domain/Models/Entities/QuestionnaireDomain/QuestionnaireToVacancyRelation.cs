using System;

namespace Recruitment.Domain.Models.Entities.QuestionnaireDomain
{
    public class QuestionnaireToVacancyRelation
    {
        public Guid QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public Guid VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
