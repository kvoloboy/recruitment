using System;
using System.Linq;
using Recruitment.BusinessLogic.Requests.Questionnaire;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.BusinessLogic.Validators
{
    public class QuestionnaireValidator
    {
        public void Validate(CreateOrUpdateQuestionnaireRequest questionnaire)
        {
            var questions = questionnaire.Questions;

            if (questions == null || !questions.Any())
            {
                throw new InvalidOperationException($"Questionnaire {questionnaire.Name} has no questions");
            }

            foreach (var question in questions)
            {
                var answers = question.Answers;

                if (answers == null || !answers.Any())
                {
                    throw new InvalidOperationException($"Question {question.Content} has no answers");
                }

                if (question.AnswerType == AnswerType.Multiple)
                {
                    continue;
                }

                var hasExactlyOneCorrectAnswer = question.Answers.Count(answer => answer.IsCorrect) == 1;

                if (!hasExactlyOneCorrectAnswer)
                {
                    throw new InvalidOperationException($"Question {question.Content}");
                }
            }
        }
    }
}
