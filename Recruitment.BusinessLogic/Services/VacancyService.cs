using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recruitment.BusinessLogic.Requests.Vacancy;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Services.Interfaces;
using Recruitment.DataAccess.Context;
using Recruitment.DataAccess.Extensions;
using Recruitment.Domain.Models.Entities;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.BusinessLogic.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly RecruitmentDbContext _recruitmentDbContext;
        private readonly IMapper _mapper;
        private readonly DbSet<Vacancy> _vacancies;
        private readonly DbSet<QuestionnaireToVacancyRelation> _questionnaireToVacancyRelations;

        public VacancyService(RecruitmentDbContext recruitmentDbContext, IMapper mapper)
        {
            _recruitmentDbContext = recruitmentDbContext;
            _mapper = mapper;
            _vacancies = _recruitmentDbContext.Vacancies;
            _questionnaireToVacancyRelations = _recruitmentDbContext.QuestionnaireToVacancyRelations;
        }

        public async Task DeleteAsync(Guid vacancyId, Guid recruiterId, bool validatePermission = true)
        {
            var vacancy = await _recruitmentDbContext.Vacancies.FindAsync(vacancyId);

            if (validatePermission)
            {
                await EnsureRecruiterCanMakeActionWithVacancy(recruiterId, vacancy);
            }

            if (!vacancy.IsActive)
            {
                throw new InvalidOperationException($"Vacancy with id {vacancyId} is already disabled");
            }

            vacancy.IsActive = false;

            await _recruitmentDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<VacancyResponseModel>> GetAllAsync(FilterVacancyRequest request)
        {
            Expression<Func<Vacancy, bool>> expression = vacancy => true;

            if (!string.IsNullOrEmpty(request.Domain))
            {
                expression = expression.AndAlso(vacancy => vacancy.Domain == request.Domain);
            }

            if (!string.IsNullOrEmpty(request.Position))
            {
                expression = expression.AndAlso(vacancy => vacancy.Position == request.Position);
            }

            var vacancies = await _vacancies.Where(expression).ToListAsync();

            if (request.Skills != null && !request.Skills.Any())
            {
                vacancies = vacancies
                    .Where(x => x.Skills.Intersect(request.Skills).Count() == request.Skills.Length)
                    .ToList();
            }

            return _mapper.Map<IEnumerable<VacancyResponseModel>>(vacancies);
        }

        public async Task<VacancyResponseModel> GetByIdAsync(Guid id)
        {
            var vacancy = await _vacancies.FindAsync(id);

            return _mapper.Map<VacancyResponseModel>(vacancy);
        }

        public async Task<Guid> CreateAsync(Guid recruiterId, CreateOrUpdateVacancyRequest request)
        {
            await _recruitmentDbContext.AssertIsExist<Recruiter>(recruiterId);

            var questionnaires = _recruitmentDbContext.Questionnaires
                .Where(questionnaire => request.Questionnaires.Contains(questionnaire.Id))
                .ToList();

            if (questionnaires.Count != request.Questionnaires.Length)
            {
                throw new InvalidOperationException("Some questionnaires wasn't found");
            }

            var vacancy = _mapper.Map<CreateOrUpdateVacancyRequest, Vacancy>(request);
            vacancy.RecruiterId = recruiterId;

            await _vacancies.AddAsync(vacancy);

            var relations = request.Questionnaires.Select(questionnaire => new QuestionnaireToVacancyRelation
            {
                VacancyId = vacancy.Id,
                QuestionnaireId = questionnaire
            });

            await _questionnaireToVacancyRelations.AddRangeAsync(relations);

            await _recruitmentDbContext.SaveChangesAsync();

            return vacancy.Id;
        }

        public async Task UpdateAsync(Guid id, Guid recruiterId, CreateOrUpdateVacancyRequest request)
        {
            var vacancy = await _recruitmentDbContext.Vacancies
                .Include(v => v.QuestionnaireToVacancyRelations)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vacancy == null)
            {
                throw new InvalidOperationException($"Not found. Vacancy id: {id}");
            }

            await EnsureRecruiterCanMakeActionWithVacancy(recruiterId, vacancy);

            _mapper.Map(request, vacancy);

            var relations = request.Questionnaires.Select(questionnaire => new QuestionnaireToVacancyRelation
                {
                    VacancyId = vacancy.Id,
                    QuestionnaireId = questionnaire
                })
                .ToList();

            var areQuestionnairesChanged = vacancy.QuestionnaireToVacancyRelations.Except(relations).Any();

            if (!areQuestionnairesChanged)
            {
                await _recruitmentDbContext.SaveChangesAsync();
                return;
            }

            _questionnaireToVacancyRelations.RemoveRange(vacancy.QuestionnaireToVacancyRelations);
            await _questionnaireToVacancyRelations.AddRangeAsync(relations);

            await _recruitmentDbContext.SaveChangesAsync();
        }

        private async Task EnsureRecruiterCanMakeActionWithVacancy(Guid recruiterId, Vacancy vacancy)
        {
            await _recruitmentDbContext.AssertIsExist<Recruiter>(recruiterId);

            if (vacancy.RecruiterId != recruiterId)
            {
                throw new InvalidOperationException($"Recruiter {recruiterId} is not allowed to modify vacancy");
            }
        }
    }
}
