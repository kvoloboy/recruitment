using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Api.Models;
using Recruitment.Api.Models.Responses;
using Recruitment.Api.Services;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        [HttpPost("sign-in")]
        public async Task<TokenResponse> Login([FromBody] LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Invalid attempt to sign in");
            }

            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == request.Email);

            return _authenticationService.GenerateToken(request.Email, appUser);
        }

        [HttpPost("sign-up")]
        public async Task<TokenResponse> Register([FromBody] RegisterRequest request)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"{string.Join(',', result.Errors.Select(e => e.Description))}");
            }

            await _signInManager.SignInAsync(user, false);

            return _authenticationService.GenerateToken(request.Email, user);
        }
    }
}
