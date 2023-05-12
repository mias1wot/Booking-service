using AutoMapper;
using BookingServiceApp.Application.Exceptions;
using BookingServiceApp.Application.Helpers;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
using BookingServiceApp.Domain.Entities;
using BookingServiceApp.Domain.Repositories;
using BookingServiceApp.Domain.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		private int? _currentUserId;

		public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_httpContextAccessor = httpContextAccessor;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public int? CurrentUserId
		{
			get
			{
				if(_currentUserId is null)
				{
					var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
					if(int.TryParse(userIdClaim, out int userId))
					{
						_currentUserId = userId;
					}
				}

				return _currentUserId;
			}
		}


		public async Task<UserDto> GetUserById(int userId)
		{
			User user = await _unitOfWork.UserRepo.GetAsync(userId);

			if (user is null)
			{
				throw new UserNotFoundException(userId);
			}


			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> GetUserByEmailAndPasswordAsync(string email, string password)
		{
			string hashedPassword = HashHelper.GetSHA256Hash(password);

			//Ardalis spec does not work!!
			//User user = await _unitOfWork.UserRepo.GetSingleAsync(new GetUserByEmailAndPassword(email, hashedPassword));
			User user = (await _unitOfWork.UserRepo.Where(user => user.Email == email && user.Password == hashedPassword)).FirstOrDefault();

			return _mapper.Map<UserDto>(user);
		}


		public async Task<UserDto> CreateUser(UserDto userDto)
		{
			User user = _mapper.Map<User>(userDto);
			user.Password = HashHelper.GetSHA256Hash(userDto.Password);

			User createdUser = await _unitOfWork.UserRepo.CreateAsync(user);
			await _unitOfWork.SaveAsync();

			return _mapper.Map<UserDto>(createdUser);
		}

		public async Task UpdateUser(UserDto userDto)
		{
			User user = await _unitOfWork.UserRepo.GetAsync(userDto.UserId);

			if(user is null)
			{
				throw new UserNotFoundException(userDto.UserId);
			}

			_mapper.Map(userDto, user);
			user.Password = HashHelper.GetSHA256Hash(userDto.Password);

			//await _unitOfWork.UserRepo.UpdateAsync(user);
			await _unitOfWork.SaveAsync();
		}

		public async Task DeleteUser(int userId)
		{
			User user = await _unitOfWork.UserRepo.GetAsync(userId);

			if (user is null)
			{
				throw new UserNotFoundException(userId);
			}


			await _unitOfWork.UserRepo.DeleteAsync(user);
			await _unitOfWork.SaveAsync();
		}
	}
}
