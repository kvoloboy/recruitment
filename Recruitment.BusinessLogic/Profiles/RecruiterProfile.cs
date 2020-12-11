using AutoMapper;
using Recruitment.BusinessLogic.Requests.Recruiter;
using Recruitment.BusinessLogic.Responses;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.BusinessLogic.Profiles
{
    public class RecruiterProfile : Profile
    {
        public RecruiterProfile()
        {
            CreateMap<CreateOrUpdateRecruiterRequest, Recruiter>(MemberList.Source);

            CreateMap<Recruiter, RecruiterResponseModel>(MemberList.Destination);
        }
    }
}
