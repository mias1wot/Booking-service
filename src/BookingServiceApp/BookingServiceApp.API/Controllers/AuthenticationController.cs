using BookingServiceApp.API.Auth;
using BookingServiceApp.API.Validators.Auth;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;
		private readonly LoginRequestValidator _loginRequestValidator;
		public AuthenticationController(IConfiguration configuration, IUserService userService, LoginRequestValidator loginRequestValidator)
		{
			_configuration = configuration;
			_userService = userService;
			_loginRequestValidator = loginRequestValidator;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			var validationRes = await _loginRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}


			UserDto user = await _userService.GetUserByEmailAndPasswordAsync(request.Email, request.Password);
			if(user is null)
			{
				return Unauthorized();
			}


			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
			var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
			var tokenOptions = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				claims: new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
					new Claim(ClaimTypes.Email, user.Email),
				},
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: signingCred);


			return Ok(new JwtTokenResponse
			{
				Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions)
			});
		}
	}
}
