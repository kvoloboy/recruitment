using System;

namespace Recruitment.Domain.Models.Entities.QuestionnaireDomain
{
    public class QuestionnaireResult : BaseEntity
    {
        public DateTime AttemptDate { get; set; }

        public double TotalMark { get; set; }

        public double MaxPossibleMark { get; set; }

        public QuestionResponse[] Answers { get; set; }

        public TimeSpan Duration { get; set; }

        public Guid QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
