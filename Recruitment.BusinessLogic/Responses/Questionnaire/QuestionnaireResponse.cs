using System.Collections.Generic;

namespace Recruitment.BusinessLogic.Responses.Questionnaire
{
    public class QuestionnaireResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<QuestionResponse> Questions { get; set; }
    }
}
