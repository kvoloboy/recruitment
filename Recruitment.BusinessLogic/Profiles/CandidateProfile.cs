using AutoMapper;
using Recruitment.BusinessLogic.Requests.Candidate;
using Recruitment.BusinessLogic.Responses;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.BusinessLogic.Profiles
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<CreateOrUpdateCandidateRequest, Candidate>(MemberList.Source);

            CreateMap<Candidate, CandidateResponseModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<Candidate, CandidateRatingResponseModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email)); ;

            CreateMap<Skill, SkillResponse>();
        }
    }
}
