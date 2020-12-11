using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Services.Interfaces;
using Recruitment.DataAccess.Context;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.BusinessLogic.Services
{
    public class SkillService : ISkillService
    {
        private readonly IMapper _mapper;
        private readonly DbSet<Skill> _skills;

        public SkillService(RecruitmentDbContext recruitmentDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _skills = recruitmentDbContext.Skills;
        }

        public async Task<IEnumerable<SkillResponse>> GetAll()
        {
            var skills = await _skills.ToListAsync();

            return _mapper.Map<List<Skill>, IEnumerable<SkillResponse>>(skills);
        }
    }
}
