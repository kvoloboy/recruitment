using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.BusinessLogic.Requests.Questionnaire;
using Recruitment.BusinessLogic.Responses.Questionnaire;
using Recruitment.BusinessLogic.Services.Interfaces;

namespace Recruitment.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/questionnaire")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        [HttpPost]
        public async Task<Guid> CreateAsync(
            [FromBody] CreateOrUpdateQuestionnaireRequest payload)
        {
            return await _questionnaireService.CreateAsync(payload);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] Guid id)
        {
            await _questionnaireService.DeleteAsync(id);
        }

        [HttpPost("{id}/result")]
        public async Task<QuestionnaireAttemptSummary> CalculateResult(
            [FromRoute] Guid id,
            [FromBody] SaveQuestionnaireAttemptRequest request)
        {
            var summary = await _questionnaireService.CalculateResult(id, request);

            return summary;
        }

        [HttpGet("{id}")]
        public async Task<QuestionnaireResponse> GetById([FromRoute] Guid id)
        {
            return await _questionnaireService.GetById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<QuestionnaireSummaryResponse>> GetAll()
        {
            return await _questionnaireService.GetAll();
        }
    }
}
