using Microsoft.AspNetCore.Identity;

namespace Recruitment.Domain.Models.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string AvatarPath { get; set; }

        public bool IsBanned { get; set; }

        public string RoleName { get; set; } = "User";
    }
}
