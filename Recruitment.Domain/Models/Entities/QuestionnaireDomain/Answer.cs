using System;

namespace Recruitment.Domain.Models.Entities.QuestionnaireDomain
{
    public class Answer  : BaseEntity
    {
        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
