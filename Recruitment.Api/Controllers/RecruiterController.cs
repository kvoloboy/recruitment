using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Api.Extensions;
using Recruitment.BusinessLogic.Requests.Recruiter;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Services.Interfaces;

namespace Recruitment.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/recruiter")]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;

        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        [HttpGet("{id}")]
        public async Task<RecruiterResponseModel> GetById([FromRoute] Guid id)
        {
            return await _recruiterService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<Guid> Create([FromBody] CreateOrUpdateRecruiterRequest request)
        {
            var userId = User.GetId();

            return await _recruiterService.RegisterAsync(userId, request);
        }

        [HttpPut("{id}")]
        public async Task Update([FromRoute] Guid id, [FromBody] CreateOrUpdateRecruiterRequest request)
        {
            await _recruiterService.UpdateInfoAsync(id, request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task Delete([FromRoute] Guid id)
        {
            await _recruiterService.RemoveAsync(id);
        }
    }
}
