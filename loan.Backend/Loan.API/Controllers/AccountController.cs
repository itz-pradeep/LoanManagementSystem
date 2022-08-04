using Loan.API.Dtos.Account;
using Loan.API.Error;
using Loan.API.Extensions;
using Loan.Core.Entities;
using Loan.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName

            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var userFromDb = await _userManager.FindByEmailAsync(loginDto.Username);

            if (userFromDb == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(userFromDb, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return new UserDto
            {
                Username = userFromDb.UserName,
                DisplayName = userFromDb.DisplayName,
                Token = _tokenService.CreateToken(userFromDb)
            };

        }
    }
}
