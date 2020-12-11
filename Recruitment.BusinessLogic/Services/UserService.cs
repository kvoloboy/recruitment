using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recruitment.BusinessLogic.Services.Interfaces;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task ToggleBanAsync(string userId)
        {
            var user = await GetById(userId);

            user.IsBanned = !user.IsBanned;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }
        }

        public async Task<bool> IsUserBanned(string userId)
        {
            var user = await GetById(userId);

            return user.IsBanned;
        }

        private async Task<User> GetById(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException($"User is not found. Id: {userId}");
            }

            return user;
        }
    }
}
