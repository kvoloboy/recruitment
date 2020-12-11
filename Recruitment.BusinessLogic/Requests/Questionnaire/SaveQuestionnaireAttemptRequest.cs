using System;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.BusinessLogic.Requests.Questionnaire
{
    public class SaveQuestionnaireAttemptRequest
    {
        public QuestionResponse[] Answers { get; set; }

        public Guid CandidateId { get; set; }

        public TimeSpan AttemptDuration { get; set; }
    }
}
