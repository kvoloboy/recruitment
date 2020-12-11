using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Api.Extensions;
using Recruitment.BusinessLogic.Requests.Candidate;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Services.Interfaces;

namespace Recruitment.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/candidate")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("apply-for-vacancy/{vacancyId}")]
        public async Task ApplyForVacancy([FromRoute] Guid vacancyId)
        {
            var userId = User.GetId();
            var candidate = await _candidateService.GetByUserId(userId);

            await _candidateService.ApplyForVacancyAsync(candidate.Id, vacancyId);
        }

        [HttpPost]
        public async Task<Guid> Create([FromBody] CreateOrUpdateCandidateRequest createRequest)
        {
            var userId = User.GetId();

            return await _candidateService.RegisterAsync(userId, createRequest);
        }

        [HttpPut("{id}")]
        public async Task Update([FromRoute] Guid id, [FromBody] CreateOrUpdateCandidateRequest request)
        {
            await _candidateService.UpdateInfoAsync(id, request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task Delete([FromRoute] Guid id)
        {
            await _candidateService.RemoveAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<CandidateResponseModel> GetById([FromRoute] Guid id)
        {
            return await _candidateService.GetById(id);
        }

        [HttpPost("by-vacancy")]
        public async Task<IEnumerable<CandidateRatingResponseModel>> GetByVacancy(
            [FromQuery] Guid vacancyId,
            [FromBody] CandidateFilterRequest filterRequest)
        {
            return await _candidateService.GetByVacancy(vacancyId, filterRequest);
        }
    }
}
