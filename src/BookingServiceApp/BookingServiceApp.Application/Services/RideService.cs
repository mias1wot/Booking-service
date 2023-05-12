using AutoMapper;
using BookingServiceApp.Application.Exceptions;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
using BookingServiceApp.Domain.Entities;
using BookingServiceApp.Domain.Repositories;
using BookingServiceApp.Domain.Specifications;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services
{
	public class RideService : IRideService
	{
		private readonly IConfiguration _configuration;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IRouteApiService _routeApiService;
		private readonly ITicketService _ticketService;
		public RideService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper, IRouteApiService routeApiService, ITicketService ticketService)
		{
			_configuration = configuration;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_routeApiService = routeApiService;
			_ticketService = ticketService;
		}

		public async Task<IEnumerable<RideDto>> GetUserRidesAsync(int userId)
		{
			// Get user with all their rides.
			User user = await _unitOfWork.UserRepo.GetSingleAsync(new GetUserWithAllRidesById(userId));

			// Check user existence.
			if (user is null)
			{
				throw new UserNotFoundException(userId);
			}


			return _mapper.Map<List<RideDto>>(user.Rides);
		}

		public async Task<IEnumerable<RouteDto>> GetAvailableRoutesAsync(RouteSearchParamsDto routeSearchParamsDto)
		{
			return await _routeApiService.GetAvailableRoutesAsync(routeSearchParamsDto);
		}

		public async Task<RideDto> BookRideAsync(int userId, BookRideParamsDto bookRideParamsDto)
		{
			// Check user existence
			User user = await _unitOfWork.UserRepo.GetAsync(userId);

			if (user is null)
			{
				throw new UserNotFoundException(userId);
			}


			// Send request to RouteService
			var rideConfirmationDto = await _routeApiService.BookRideAsync(bookRideParamsDto);


			// Generate a ticket code
			string ticket = await _ticketService.GenerateTicket(userId, rideConfirmationDto);


			// Persist to database
			Ride ride = _mapper.Map<Ride>(rideConfirmationDto);
			ride.TicketCode = ticket;

			// They yield the same result? todo!! error-prone!! You need to check whether this single query creates both rides and seats.
			// 1
			ride.User = user;
			await _unitOfWork.RideRepo.CreateAsync(ride);
			// 2
			//user.Rides.Add(ride);


			await _unitOfWork.SaveAsync();

			
			RideDto rideDto = _mapper.Map<RideDto>(ride);

			return rideDto;
		}
	}
}
