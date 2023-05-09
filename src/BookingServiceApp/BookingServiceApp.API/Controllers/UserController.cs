using AutoMapper;
using BookingServiceApp.API.BookingService.Responses;
using BookingServiceApp.API.User.Requests;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
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
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		public UserController(IUserService userService, IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<UserResponse>> GetUserByid(int id)
		{
			UserDto userDto = await _userService.GetUserById(id);

			return Ok(_mapper.Map<UserResponse>(userDto));
		}

		[HttpPost]
		public async Task<ActionResult<UserResponse>> CreateUser(CreateUserRequest request)
		{
			UserDto userDto = _mapper.Map<UserDto>(request);

			UserDto createdUserDto = await _userService.CreateUser(userDto, request.Password);

			UserResponse response = _mapper.Map<UserResponse>(createdUserDto);

			return CreatedAtAction("GetUserByid", new { id = createdUserDto.Id }, response);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUser(UpdateUserRequest request)
		{
			UserDto userDto = _mapper.Map<UserDto>(request);

			await _userService.UpdateUser(userDto);

			return NoContent();
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteUser(int id)
		{
			await _userService.DeleteUser(id);

			return NoContent();
		}
	}
}
