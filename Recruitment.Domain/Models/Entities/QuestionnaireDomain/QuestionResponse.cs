using System;

namespace Recruitment.Domain.Models.Entities.QuestionnaireDomain
{
    public class QuestionResponse
    {
        public Guid QuestionId { get; set; }

        public Guid AnswerId { get; set; }
    }
}
