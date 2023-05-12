﻿using AutoMapper;
using BookingServiceApp.API.Filters;
using BookingServiceApp.API.Ride.Requests;
using BookingServiceApp.API.Ride.Responses;
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
	public class RideController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IRideService _rideService;
		private readonly ITicketService _ticketService;
		private readonly IMapper _mapper;
		private readonly IValidator<BookRideRequest> _bookRideRequestValidator;
		private readonly IValidator<GetAvailableRoutesRequest> _getAvailableRoutesRequestValidator;

		public RideController(IUserService userService, IRideService rideService, ITicketService ticketService, IMapper mapper, IValidator<BookRideRequest> bookRideRequestValidator, IValidator<GetAvailableRoutesRequest> getAvailableRoutesRequestValidator)
		{
			_userService = userService;
			_rideService = rideService;
			_ticketService = ticketService;
			_mapper = mapper;
			_bookRideRequestValidator = bookRideRequestValidator;
			_getAvailableRoutesRequestValidator = getAvailableRoutesRequestValidator;
		}


		[HttpGet]
		public async Task<ActionResult<GetUserRidesResponse>> GetUserRidesAsync()
		{
			int? userId = _userService.CurrentUserId;
			if (userId == null)
			{
				return Unauthorized();
			}


			IEnumerable<RideDto> rideDtos = await _rideService.GetUserRidesAsync((int)userId);

			return Ok(new GetUserRidesResponse {
				Rides = _mapper.Map<List<RideResponse>>(rideDtos)
			});
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<GetAvailableRoutesResponse>> GetAvailableRoutes(GetAvailableRoutesRequest request)
		{
			var validationRes = await _getAvailableRoutesRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}


			RouteSearchParamsDto routeSearchParamsDto = _mapper.Map<RouteSearchParamsDto>(request);

			IEnumerable<RouteDto> routeDtos = await _rideService.GetAvailableRoutesAsync(routeSearchParamsDto);

			return Ok(new GetAvailableRoutesResponse
			{
				Routes = _mapper.Map<List<AvailableRoute>>(routeDtos)
			});
		}

		[HttpPost]
		public async Task<ActionResult<BookRideResponse>> BookRide(BookRideRequest request)
		{
			int? userId = _userService.CurrentUserId;
			if (userId == null)
			{
				return Unauthorized();
			}


			var validationRes = await _bookRideRequestValidator.ValidateAsync(request);
			if (!validationRes.IsValid)
			{
				return BadRequest(validationRes.Errors);
			}


			BookRideParamsDto bookRideParamsDto = _mapper.Map<BookRideParamsDto>(request);

			RideDto rideDto = await _rideService.BookRideAsync((int)userId, bookRideParamsDto);

			return Ok(_mapper.Map<BookRideResponse>(rideDto));
		}

		[HttpGet]
		public async Task<ActionResult<bool>> ValidateTicketCode(string ticketCode)
		{
			int? userId = _userService.CurrentUserId;
			if (userId == null)
			{
				return Unauthorized();
			}


			return Ok(await _ticketService.IsValid((int)userId, ticketCode));
		}
	}
}
