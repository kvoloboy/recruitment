namespace Recruitment.BusinessLogic.Requests.Questionnaire
{
    public class CreateOrUpdateAnswerRequest
    {
        public string Content { get; set; }

        public bool IsCorrect { get; set; }
    }
}
