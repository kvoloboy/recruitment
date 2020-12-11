using System.Threading.Tasks;

namespace Recruitment.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task ToggleBanAsync(string userId);

        Task<bool> IsUserBanned(string userId);
    }
}
