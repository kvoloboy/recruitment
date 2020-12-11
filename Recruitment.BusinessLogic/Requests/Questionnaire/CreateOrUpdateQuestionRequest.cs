using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.BusinessLogic.Requests.Questionnaire
{
    public class CreateOrUpdateQuestionRequest
    {
        [Required]
        public string Content { get; set; }

        public AnswerType AnswerType { get; set; }

        public double Value { get; set; }

        public ICollection<CreateOrUpdateAnswerRequest> Answers { get; set; }
    }
}
