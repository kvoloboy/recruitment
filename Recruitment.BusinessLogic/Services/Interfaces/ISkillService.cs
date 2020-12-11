using System.Collections.Generic;
using System.Threading.Tasks;
using Recruitment.BusinessLogic.Responses;

namespace Recruitment.BusinessLogic.Services.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillResponse>> GetAll();
    }
}
