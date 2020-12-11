using System;
using System.Collections.Generic;

namespace Recruitment.Domain.Models.Entities.QuestionnaireDomain
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }

        public AnswerType AnswerType { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public double Value { get; set; }

        public Guid QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}
