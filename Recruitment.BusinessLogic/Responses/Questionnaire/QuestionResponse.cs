using System;
using System.Collections.Generic;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.BusinessLogic.Responses.Questionnaire
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public AnswerType AnswerType { get; set; }

        public IEnumerable<AnswerResponse> Answers { get; set; }
    }
}
