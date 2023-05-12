using AutoMapper;
using BookingServiceApp.API.BookingService.Responses;
using BookingServiceApp.API.Filters;
using BookingServiceApp.API.User.Requests;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[ApiExceptionFilter]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly IValidator<CreateUserRequest> _createUserRequestValidator;
		private readonly IValidator<UpdateUserRequest> _updateUserRequestValidator;

		public UserController(IUserService userService, IMapper mapper, IValidator<CreateUserRequest> createUserRequestValidator, IValidator<UpdateUserRequest> updateUserRequestValidator)		{
			_userService = userService;
			_mapper = mapper;
			_createUserRequestValidator = createUserRequestValidator;
			_updateUserRequestValidator = updateUserRequestValidator;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<UserResponse>> GetUserByid(int id)
		{
			UserDto userDto = await _userService.GetUserById(id);

			return Ok(_mapper.Map<UserResponse>(userDto));
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult<UserResponse>> CreateUser(CreateUserRequest request)
		{
			var validationRes = await _createUserRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}


			UserDto userDto = _mapper.Map<UserDto>(request);

			UserDto createdUserDto = await _userService.CreateUser(userDto);

			UserResponse response = _mapper.Map<UserResponse>(createdUserDto);

			return CreatedAtAction("GetUserByid", new { id = createdUserDto.UserId }, response);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUser(UpdateUserRequest request)
		{
			int? userId = _userService.CurrentUserId;
			if(userId == null)
			{
				return Unauthorized();
			}


			var validationRes = await _updateUserRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}


			UserDto userDto = _mapper.Map<UserDto>(request);
			userDto.UserId = (int)userId;

			await _userService.UpdateUser(userDto);

			return NoContent();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteUser()
		{
			int? userId = _userService.CurrentUserId;
			if (userId == null)
			{
				return Unauthorized();
			}


			await _userService.DeleteUser((int)userId);

			return NoContent();
		}
	}
}
