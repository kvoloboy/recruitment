using Recruitment.Api.Models.Responses;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.Api.Services
{
    public interface IAuthenticationService
    {
        TokenResponse GenerateToken(string email, User user);
    }
}
