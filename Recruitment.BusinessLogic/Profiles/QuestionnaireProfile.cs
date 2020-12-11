using AutoMapper;
using Recruitment.BusinessLogic.Requests.Questionnaire;
using Recruitment.BusinessLogic.Responses.Questionnaire;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;
using QuestionResponse = Recruitment.BusinessLogic.Responses.Questionnaire.QuestionResponse;

namespace Recruitment.BusinessLogic.Profiles
{
    public class QuestionnaireProfile : Profile
    {
        public QuestionnaireProfile()
        {
            CreateMap<CreateOrUpdateQuestionnaireRequest, Questionnaire>(MemberList.Source);

            CreateMap<CreateOrUpdateQuestionRequest, Question>(MemberList.Source);

            CreateMap<CreateOrUpdateAnswerRequest, Answer>(MemberList.Source);

            CreateMap<Answer, AnswerResponse>(MemberList.Destination);

            CreateMap<Questionnaire, QuestionnaireResponse>(MemberList.Destination);

            CreateMap<Question, QuestionResponse>(MemberList.Destination);

            CreateMap<Questionnaire, QuestionnaireSummaryResponse>(MemberList.Destination);
        }
    }
}
