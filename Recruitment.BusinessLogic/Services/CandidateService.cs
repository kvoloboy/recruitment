using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recruitment.BusinessLogic.Requests.Candidate;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Services.Interfaces;
using Recruitment.DataAccess.Context;
using Recruitment.DataAccess.Extensions;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.BusinessLogic.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly RecruitmentDbContext _recruitmentDbContext;
        private readonly DbSet<VacancyApplication> _vacancyApplications;
        private readonly DbSet<Vacancy> _vacancies;
        private readonly DbSet<Candidate> _candidates;
        private readonly IMapper _mapper;

        public CandidateService(RecruitmentDbContext recruitmentDbContext, IMapper mapper)
        {
            _recruitmentDbContext = recruitmentDbContext;
            _mapper = mapper;
            _vacancyApplications = _recruitmentDbContext.VacancyApplications;
            _candidates = _recruitmentDbContext.Candidates;
            _vacancies = _recruitmentDbContext.Vacancies;
        }

        public async Task<CandidateResponseModel> GetById(Guid id)
        {
            var candidate = await GetEntityById(id, true);

            return _mapper.Map<Candidate, CandidateResponseModel>(candidate);
        }

        public async Task<CandidateResponseModel> GetByUserId(string id)
        {
            var candidate = await _candidates.FirstOrDefaultAsync(c => c.UserId == id);

            if (candidate == null)
            {
                throw new InvalidOperationException("Not found");
            }

            return _mapper.Map<Candidate, CandidateResponseModel>(candidate);
        }

        public async Task<IEnumerable<CandidateRatingResponseModel>> GetByVacancy(
            Guid vacancyId,
            CandidateFilterRequest filterModel)
        {
            var vacancy = await _vacancies
                .Include(x => x.QuestionnaireToVacancyRelations)
                .FirstOrDefaultAsync(x => x.Id == vacancyId);

            if (vacancy == null)
            {
                throw new InvalidOperationException($"Vacancy is not found. Id: {vacancyId}");
            }

            var appliedCandidates = await _vacancyApplications.Where(application => application.VacancyId == vacancyId)
                .ToListAsync();

            var candidateIds = appliedCandidates.Select(application => application.CandidateId).ToArray();

            var candidates = await FilterCandidates(candidateIds, filterModel);

            var questionnaires = vacancy.QuestionnaireToVacancyRelations.Select(x => x.QuestionnaireId).ToList();

            var candidatesWithQuizPoints = candidates.Select(x =>
            {
                var points = x.QuestionnaireResults
                    .Where(result => questionnaires.Contains(result.QuestionnaireId))
                    .Sum(result => result.TotalMark);

                return (x, points);
            });

            var responses = new List<CandidateRatingResponseModel>();

            foreach (var (candidate, points) in candidatesWithQuizPoints)
            {
                var responseModel = _mapper.Map<Candidate, CandidateRatingResponseModel>(candidate);
                responseModel.Points = points;

                responses.Add(responseModel);
            }

            return responses;
        }

        private async Task<List<Candidate>> FilterCandidates(
            Guid[] appliedCandidates,
            CandidateFilterRequest filterModel)
        {
            Expression<Func<Candidate, bool>> predicate = candidate => true;

            if (appliedCandidates != null && appliedCandidates.Any())
            {
                predicate = predicate.AndAlso(candidate => appliedCandidates.Contains(candidate.Id));
            }

            if (!string.IsNullOrEmpty(filterModel.Domain))
            {
                predicate = predicate.AndAlso(candidate => candidate.Domain == filterModel.Domain);
            }

            if (!string.IsNullOrEmpty(filterModel.Position))
            {
                predicate = predicate.AndAlso(candidate => candidate.Position == filterModel.Position);
            }

            if (filterModel.MinExpectedSalary != null)
            {
                predicate = predicate.AndAlso(candidate => candidate.ExpectedSalary >= filterModel.MinExpectedSalary);
            }

            if (filterModel.MaxExpectedSalary != null)
            {
                predicate = predicate.AndAlso(candidate => candidate.ExpectedSalary <= filterModel.MaxExpectedSalary);
            }

            if (filterModel.ExperienceInYears != null)
            {
                predicate = predicate.AndAlso(candidate => candidate.ExperienceInYears >= filterModel.ExperienceInYears);
            }

            var candidates = await _candidates
                .Include(x => x.User)
                .Include(x => x.QuestionnaireResults)
                .Where(predicate)
                .ToListAsync();

            if (filterModel.Skills != null && !filterModel.Skills.Any())
            {
                candidates = candidates
                    .Where(x => x.Skills.Intersect(filterModel.Skills).Count() == filterModel.Skills.Length)
                    .ToList();
            }

            return candidates;
        }

        public async Task<Guid> RegisterAsync(string userId, CreateOrUpdateCandidateRequest request)
        {
            var candidate = _mapper.Map<CreateOrUpdateCandidateRequest, Candidate>(request);
            candidate.UserId = userId;

            await _candidates.AddAsync(candidate);

            await _recruitmentDbContext.SaveChangesAsync();

            return candidate.Id;
        }

        public async Task UpdateInfoAsync(Guid candidateId, CreateOrUpdateCandidateRequest request)
        {
            var candidate = await GetEntityById(candidateId);

            _mapper.Map(request, candidate);

            await _recruitmentDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var candidate = await GetEntityById(id);

            _candidates.Remove(candidate);
        }

        public async Task ApplyForVacancyAsync(Guid candidateId, Guid vacancyId)
        {
            await _recruitmentDbContext.AssertIsExist<Candidate>(candidateId);

            await _recruitmentDbContext.AssertIsExist<Vacancy>(vacancyId);

            var isAlreadyApplied = await _vacancyApplications.AnyAsync(application =>
                application.CandidateId == candidateId && application.VacancyId == vacancyId);

            if (isAlreadyApplied)
            {
                throw new InvalidOperationException("Already applied");
            }

            await _vacancyApplications.AddAsync(new VacancyApplication
            {
                CandidateId = candidateId,
                VacancyId = vacancyId
            });

            await _recruitmentDbContext.SaveChangesAsync();
        }

        private async Task<Candidate> GetEntityById(Guid id, bool includeUserData = false)
        {

            Candidate candidate;

            if (includeUserData)
            {
                candidate = await _candidates.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                candidate = await _candidates.FindAsync(id);
            }

            if (candidate == null)
            {
                throw new InvalidOperationException("Not found");
            }

            return candidate;
        }
    }
}
