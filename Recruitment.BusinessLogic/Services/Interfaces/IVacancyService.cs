using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recruitment.BusinessLogic.Requests.Vacancy;
using Recruitment.BusinessLogic.Responses;

namespace Recruitment.BusinessLogic.Services.Interfaces
{
    public interface IVacancyService
    {
        Task<Guid> CreateAsync(Guid recruiterId, CreateOrUpdateVacancyRequest request);

        Task UpdateAsync(Guid id, Guid recruiterId, CreateOrUpdateVacancyRequest request);

        Task DeleteAsync(Guid vacancyId, Guid recruiterId);

        Task<IEnumerable<VacancyResponseModel>> GetAllAsync(FilterVacancyRequest request);

        Task<VacancyResponseModel> GetByIdAsync(Guid id);
    }
}
