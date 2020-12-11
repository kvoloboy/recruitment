using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Api.Extensions;
using Recruitment.BusinessLogic.Requests.Vacancy;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Services.Interfaces;

namespace Recruitment.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/vacancy")]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        private readonly IRecruiterService _recruiterService;

        public VacancyController(IVacancyService vacancyService, IRecruiterService recruiterService)
        {
            _vacancyService = vacancyService;
            _recruiterService = recruiterService;
        }

        [HttpPost]
        public async Task<Guid> Create(CreateOrUpdateVacancyRequest createVacancy)
        {
            var userId = User.GetId();
            var recruiter = await _recruiterService.GetByUserId(userId);

            var vacancyId = await _vacancyService.CreateAsync(recruiter.Id, createVacancy);

            return vacancyId;
        }

        [HttpPut("{vacancyId}")]
        public async Task Update(
            [FromRoute] Guid vacancyId,
            CreateOrUpdateVacancyRequest updateVacancy)
        {
            var userId = User.GetId();
            var recruiter = await _recruiterService.GetByUserId(userId);

            await _vacancyService.UpdateAsync(vacancyId, recruiter.Id, updateVacancy);
        }

        [HttpDelete("{vacancyId}")]
        public async Task Disable([FromRoute] Guid vacancyId)
        {
            var userId = User.GetId();
            var recruiter = await _recruiterService.GetByUserId(userId);

            var validatePermission = !User.IsInRole("Admin");

            await _vacancyService.DeleteAsync(vacancyId, recruiter.Id, validatePermission);
        }

        [HttpPost("find")]
        public async Task<IEnumerable<VacancyResponseModel>> GetAll(
            [FromBody] FilterVacancyRequest filterVacancyRequest)
        {
            return await _vacancyService.GetAllAsync(filterVacancyRequest);
        }

        [HttpGet("{id}")]
        public async Task<VacancyResponseModel> GetById([FromRoute] Guid id)
        {
            return await _vacancyService.GetByIdAsync(id);
        }
    }
}
