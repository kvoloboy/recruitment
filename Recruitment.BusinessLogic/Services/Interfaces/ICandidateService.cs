using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recruitment.BusinessLogic.Requests.Candidate;
using Recruitment.BusinessLogic.Responses;

namespace Recruitment.BusinessLogic.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<CandidateResponseModel> GetById(Guid id);

        Task<CandidateResponseModel> GetByUserId(string id);

        Task<IEnumerable<CandidateRatingResponseModel>> GetByVacancy(
            Guid vacancyId,
            CandidateFilterRequest filterModel);

        Task<Guid> RegisterAsync(string userId, CreateOrUpdateCandidateRequest request);

        Task UpdateInfoAsync(Guid candidateId, CreateOrUpdateCandidateRequest request);

        Task RemoveAsync(Guid id);

        Task ApplyForVacancyAsync(Guid candidateId, Guid vacancyId);
    }
}
