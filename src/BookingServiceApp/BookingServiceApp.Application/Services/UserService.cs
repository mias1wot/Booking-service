using AutoMapper;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
using BookingServiceApp.Domain.Entities;
using BookingServiceApp.Domain.Repositories;
using BookingServiceApp.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public UserService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<UserDto> GetUserById(int userId)
		{
			User user = await _unitOfWork.UserRepo.GetAsync(userId);

			if (user is null)
				throw new ArgumentException("User not found.");


			return _mapper.Map<UserDto>(user);
		}


		public async Task<UserDto> CreateUser(UserDto userDto, string password)
		{
			User user = _mapper.Map<User>(userDto);
			user.Password = password;

			User createdUser = await _unitOfWork.UserRepo.CreateAsync(user);
			await _unitOfWork.SaveAsync();

			return _mapper.Map<UserDto>(createdUser);
		}

		//Can we change the password? Now we cannot - todo!!
		public async Task UpdateUser(UserDto userDto)
		{
			User user = await _unitOfWork.UserRepo.GetAsync(userDto.Id);

			if(user is null)
				throw new ArgumentException($"User with id {userDto.Id} not found");

			_mapper.Map(userDto, user);

			//await _unitOfWork.UserRepo.UpdateAsync(user);
			await _unitOfWork.SaveAsync();
		}

		public async Task DeleteUser(int userId)
		{
			User user = await _unitOfWork.UserRepo.GetAsync(userId);

			if (user is null)
				throw new ArgumentException($"User with id {userId} not found");


			await _unitOfWork.UserRepo.DeleteAsync(user);
			await _unitOfWork.SaveAsync();
		}
	}
}
