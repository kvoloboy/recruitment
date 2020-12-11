using AutoMapper;
using Recruitment.BusinessLogic.Requests.Vacancy;
using Recruitment.BusinessLogic.Responses;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.BusinessLogic.Profiles
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<CreateOrUpdateVacancyRequest, Vacancy>(MemberList.Source);

            CreateMap<Vacancy, VacancyResponseModel>(MemberList.Destination);
        }
    }
}
