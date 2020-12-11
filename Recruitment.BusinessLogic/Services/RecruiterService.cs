using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recruitment.BusinessLogic.Requests.Recruiter;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Services.Interfaces;
using Recruitment.DataAccess.Context;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.BusinessLogic.Services
{
    public class RecruiterService : IRecruiterService
    {
        private readonly RecruitmentDbContext _recruitmentDbContext;
        private readonly IMapper _mapper;
        private readonly DbSet<Recruiter> _recruiters;

        public RecruiterService(RecruitmentDbContext recruitmentDbContext, IMapper mapper)
        {
            _recruitmentDbContext = recruitmentDbContext;
            _mapper = mapper;
            _recruiters = recruitmentDbContext.Recruiters;
        }

        public async Task<RecruiterResponseModel> GetByIdAsync(Guid id)
        {
            var recruiter = await _recruiters.FindAsync(id);

            return _mapper.Map<RecruiterResponseModel>(recruiter);
        }

        public async Task<RecruiterResponseModel> GetByUserId(string id)
        {
            var recruiter = await _recruiters.FirstOrDefaultAsync(r => r.UserId == id);

            if (recruiter == null)
            {
                throw new InvalidOperationException("Not found");
            }

            return _mapper.Map<RecruiterResponseModel>(recruiter);
        }

        public async Task<Guid> RegisterAsync(string userId, CreateOrUpdateRecruiterRequest request)
        {
            var recruiter = _mapper.Map<CreateOrUpdateRecruiterRequest, Recruiter>(request);
            recruiter.UserId = userId;

            await _recruiters.AddAsync(recruiter);
            await _recruitmentDbContext.SaveChangesAsync();

            return recruiter.Id;
        }

        public async Task UpdateInfoAsync(Guid id, CreateOrUpdateRecruiterRequest request)
        {
            var recruiter = await FindEntityById(id);

            _mapper.Map(request, recruiter);

            await _recruitmentDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var recruiter = await FindEntityById(id);

            _recruiters.Remove(recruiter);

            await _recruitmentDbContext.SaveChangesAsync();
        }

        private async Task<Recruiter> FindEntityById(Guid id)
        {
            var recruiter = await _recruiters.FindAsync(id);

            if (recruiter == null)
            {
                throw new InvalidOperationException("Not found");
            }

            return recruiter;
        }
    }
}
