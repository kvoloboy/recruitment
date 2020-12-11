using Microsoft.AspNetCore.Identity;
using Recruitment.Api.Models.Responses;

namespace Recruitment.Api.Services
{
    public interface IAuthenticationService
    {
        TokenResponse GenerateToken(string email, IdentityUser user);
    }
}
