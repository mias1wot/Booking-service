using AutoMapper;
using BookingServiceApp.Application.Exceptions;
using BookingServiceApp.Application.Helpers;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
using BookingServiceApp.Domain.Entities;
using BookingServiceApp.Domain.Repositories;
using BookingServiceApp.Domain.Specifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services
{
	public class TicketService : ITicketService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<string> GenerateTicket(int userId, RideConfirmationDto rideConfirmationDto)
		{
			User user = await _unitOfWork.UserRepo.GetAsync(userId);

			if (user is null)
			{
				throw new UserNotFoundException(userId);
			}

			// Get password hash
			string passHash = HashHelper.GetSHA256Hash(user.Password);


			// Alter rideConfirmationDto to make Validation possible.
			var savedErrors = rideConfirmationDto.Errors;
			rideConfirmationDto.Errors = null;

			// Get hash of altered RideConfirmationDto.
			string rideDtoHash = HashHelper.GetSHA256Hash(JsonConvert.SerializeObject(rideConfirmationDto));

			// Revert back the rideConfirmationDto changes.
			rideConfirmationDto.Errors = savedErrors;


			return passHash + rideDtoHash;
		}

		public async Task<bool> IsValid(int userId, string ticket)
		{
			// Check user existence
			User user = await _unitOfWork.UserRepo.GetSingleAsync(new GetUserWithAllRidesById(userId));

			if (user is null)
			{
				throw new UserNotFoundException(userId);
			}

			// Get password hash
			string passHash = HashHelper.GetSHA256Hash(user.Password);

			// Traverse all user rides. If hash of password and generated rideConfirmationDto from ride equal to ticket, the ticket is valied. Otherwise not.
			RideConfirmationDto rideConfirmationDto;
			string rideDtoHash;
			foreach (Ride ride in user.Rides)
			{
				rideConfirmationDto = _mapper.Map<RideConfirmationDto>(ride);
				rideConfirmationDto.IsSuccess = true;
				rideDtoHash = HashHelper.GetSHA256Hash(JsonConvert.SerializeObject(rideConfirmationDto));
				if (passHash + rideDtoHash == ticket)
					return true;
			}

			return false;
		}
	}
}
