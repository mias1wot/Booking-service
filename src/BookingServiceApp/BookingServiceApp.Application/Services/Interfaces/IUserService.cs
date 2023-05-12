using BookingServiceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services.Interfaces
{
	public interface IUserService
	{
		int? CurrentUserId { get; }


		Task<UserDto> GetUserById(int userId);
		Task<UserDto> GetUserByEmailAndPasswordAsync(string email, string password);
		Task<UserDto> CreateUser(UserDto userDto);
		Task UpdateUser(UserDto userDto);
		Task DeleteUser(int userId);
	}
}
