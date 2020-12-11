using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recruitment.BusinessLogic.Requests.Questionnaire;
using Recruitment.BusinessLogic.Responses;
using Recruitment.BusinessLogic.Responses.Questionnaire;
using Recruitment.BusinessLogic.Services.Interfaces;
using Recruitment.BusinessLogic.Validators;
using Recruitment.DataAccess.Context;
using Recruitment.DataAccess.Extensions;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.BusinessLogic.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly QuestionnaireValidator _questionnaireValidator;
        private readonly RecruitmentDbContext _recruitmentDbContext;
        private readonly DbSet<Questionnaire> _questionnaireDbSet;
        private readonly DbSet<Question> _questionDbSet;
        private readonly DbSet<QuestionnaireResult> _questionnaireResults;
        private readonly IMapper _mapper;

        public QuestionnaireService(
            QuestionnaireValidator questionnaireValidator,
            RecruitmentDbContext recruitmentDbContext,
            IMapper mapper)
        {
            _questionnaireValidator = questionnaireValidator;
            _recruitmentDbContext = recruitmentDbContext;
            _questionnaireDbSet = recruitmentDbContext.Questionnaires;
            _questionDbSet = recruitmentDbContext.Questions;
            _questionnaireResults = recruitmentDbContext.QuestionnaireResults;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateOrUpdateQuestionnaireRequest request)
        {
            _questionnaireValidator.Validate(request);
            var questionnaire = _mapper.Map<CreateOrUpdateQuestionnaireRequest, Questionnaire>(request);

            await _questionnaireDbSet.AddAsync(questionnaire);
            await _recruitmentDbContext.SaveChangesAsync();

            return questionnaire.Id;
        }

        public async Task<QuestionnaireResponse> GetById(Guid id)
        {
            var questionnaire = await _questionnaireDbSet.Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (questionnaire == null)
            {
                throw new InvalidOperationException("Not found");
            }

            return _mapper.Map<Questionnaire, QuestionnaireResponse>(questionnaire);
        }

        public async Task<IEnumerable<QuestionnaireSummaryResponse>> GetAll()
        {
            var questionnaires = await _questionnaireDbSet.ToListAsync();
            return _mapper.Map<List<QuestionnaireSummaryResponse>>(questionnaires);
        }

        public async Task DeleteAsync(Guid id)
        {
            var questionnaire = await _questionnaireDbSet.FindAsync(id);

            if (questionnaire == null)
            {
                throw new InvalidOperationException($"Not found. Id: {id}");
            }

            _questionnaireDbSet.Remove(questionnaire);

            await _recruitmentDbContext.SaveChangesAsync();
        }

        public async Task<QuestionnaireAttemptSummary> CalculateResult(Guid id, SaveQuestionnaireAttemptRequest request)
        {
            await _recruitmentDbContext.AssertIsExist<Questionnaire>(id);

            var questions = _questionDbSet
                .Include(q => q.Answers)
                .Where(q => q.QuestionnaireId == id);

            var questionnaireResult = new QuestionnaireResult
            {
                CandidateId = request.CandidateId,
                QuestionnaireId = id,
                Duration = request.AttemptDuration,
                Answers = request.Answers,
                AttemptDate = DateTime.UtcNow
            };

            foreach (var question in questions)
            {
                var correctAnswers = question.Answers.Where(a => a.IsCorrect)
                    .Select(x => x.Id);

                var responseAnswers = request.Answers.Where(response => response.QuestionId == question.Id);

                var isUserAnswerCorrect = responseAnswers.All(answer => correctAnswers.Contains(answer.AnswerId));

                if (isUserAnswerCorrect)
                {
                    questionnaireResult.TotalMark += question.Value;
                }
            }

            questionnaireResult.MaxPossibleMark = questions.Sum(q => q.Value);

            await _questionnaireResults.AddAsync(questionnaireResult);
            await _recruitmentDbContext.SaveChangesAsync();

            return new QuestionnaireAttemptSummary
            {
                Mark = questionnaireResult.TotalMark,
                MaxMark = questionnaireResult.MaxPossibleMark
            };
        }
    }
}
