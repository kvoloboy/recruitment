using System;
using System.Threading.Tasks;
using Recruitment.BusinessLogic.Requests.Recruiter;
using Recruitment.BusinessLogic.Responses;

namespace Recruitment.BusinessLogic.Services.Interfaces
{
    public interface IRecruiterService
    {
        Task<RecruiterResponseModel> GetByIdAsync(Guid id);

        Task<RecruiterResponseModel> GetByUserId(string id);

        Task<Guid> RegisterAsync(string userId, CreateOrUpdateRecruiterRequest request);

        Task UpdateInfoAsync(Guid id, CreateOrUpdateRecruiterRequest request);

        Task RemoveAsync(Guid id);
    }
}
