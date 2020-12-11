using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            return users;
        }

        [HttpPut("{userId}")]
        public async Task<IdentityResult> UpdateAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                return await _userManager.UpdateAsync(user);
            }

            return IdentityResult.Failed();
        }

        [HttpDelete("{userId}")]
        public async Task<IdentityResult> Delete(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }

            return IdentityResult.Failed();
        }
    }
}
