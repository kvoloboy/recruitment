using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recruitment.BusinessLogic.Requests.Questionnaire;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Responses.Questionnaire;

namespace Recruitment.BusinessLogic.Services.Interfaces
{
    public interface IQuestionnaireService
    {
        Task<Guid> CreateAsync(CreateOrUpdateQuestionnaireRequest request);

        Task<QuestionnaireResponse> GetById(Guid id);

        Task<IEnumerable<QuestionnaireSummaryResponse>> GetAll();

        Task DeleteAsync(Guid id);

        Task<QuestionnaireAttemptSummary> CalculateResult(Guid id, SaveQuestionnaireAttemptRequest request);
    }
}
