﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NZWalks.API.Repositories
{
	public class TokenRepository : ITokenRepository
	{
		private readonly IConfiguration configuration;

		public TokenRepository(IConfiguration configuration)
        {
			this.configuration = configuration;
		}
        public string CreateJWTToken(IdentityUser user, List<string> roles)
		{
			var claims = new List<Claim>();
			
			claims.Add(new Claim(ClaimTypes.Email, user.Email));

			foreach(var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
			var cerdentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var Token = new JwtSecurityToken(
				configuration["JWT:Issuer"],

				configuration["JWT:Audience"],
				claims,
				expires : DateTime.Now.AddMinutes(15),
				signingCredentials: cerdentials);

			return new JwtSecurityTokenHandler().WriteToken(Token);
		}
		
	}
}
