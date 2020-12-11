using System.Linq;
using System.Security.Claims;

namespace Recruitment.Api.Extensions
{
    public static class UserExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            var idClaim = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier);

            return idClaim.Value;
        }
    }
}
