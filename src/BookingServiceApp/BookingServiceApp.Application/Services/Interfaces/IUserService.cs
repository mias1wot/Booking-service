﻿using BookingServiceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services.Interfaces
{
	public interface IUserService
	{
		Task<UserDto> GetUserById(int userId);
		Task<UserDto> CreateUser(UserDto userDto, string password);
		Task UpdateUser(UserDto userDto);
		Task DeleteUser(int userId);
	}
}
