using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recruitment.BusinessLogic.Requests.Questionnaire
{
    public class CreateOrUpdateQuestionnaireRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<CreateOrUpdateQuestionRequest> Questions { get; set; }
    }
}
