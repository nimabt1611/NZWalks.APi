using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly ITokenRepository tokenRepository;

		public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository )
        {
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
		}

        [HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
		{
			var identityuser = new IdentityUser
			{
				UserName = registerRequestDto.Username,
				Email = registerRequestDto.Username

			};

			var identityResualt = await userManager.CreateAsync(identityuser, registerRequestDto.Password);

			if(identityResualt.Succeeded)
			{
				if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
				{
					identityResualt = await userManager.AddToRolesAsync(identityuser, registerRequestDto.Roles);
				}
			}
			return Ok("your succeeded register please login!");
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

			if (user != null)
			{
				var CheckPassword = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
				if(CheckPassword)
				{
					var roles = await userManager.GetRolesAsync(user);
					if(roles != null)
					{
						var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

						var response = new LoginResponseDto
						{
							JwtToken = jwtToken
						};
						return Ok(response); 
					}
					
				}
			}
			return BadRequest("You can't Login");
		}
	}
}
