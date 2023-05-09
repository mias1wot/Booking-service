using AutoMapper;
using BookingServiceApp.API.Ride.Requests;
using BookingServiceApp.API.Ride.Responses;
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
	public class RideController : ControllerBase
	{
		private readonly IRideService _rideService;
		private readonly ITicketService _ticketService;
		private readonly IMapper _mapper;
		public RideController(IRideService rideService, ITicketService ticketService, IMapper mapper)
		{
			_rideService = rideService;
			_ticketService = ticketService;
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<ActionResult<GetUserRidesResponse>> GetUserRidesAsync(int userId)
		{
			IEnumerable<RideDto> rideDtos = await _rideService.GetUserRidesAsync(userId);

			return Ok(new GetUserRidesResponse {
				Rides = _mapper.Map<List<RideResponse>>(rideDtos)
			});
		}

		[HttpGet]
		public async Task<ActionResult<GetAvailableRoutesResponse>> GetAvailableRoutes(GetAvailableRoutesRequest request)
		{
			RouteSearchParamsDto routeSearchParamsDto = _mapper.Map<RouteSearchParamsDto>(request);

			IEnumerable<RouteDto> routeDtos = await _rideService.GetAvailableRoutesAsync(routeSearchParamsDto);

			return Ok(new GetAvailableRoutesResponse
			{
				Routes = _mapper.Map<List<AvailableRoute>>(routeDtos)
			});
		}

		[HttpPost]
		public async Task<ActionResult<BookRideResponse>> BookRide(int userId, BookRideRequest request)
		{
			BookRideParamsDto bookRideParamsDto = _mapper.Map<BookRideParamsDto>(request);

			RideDto rideDto = await _rideService.BookRideAsync(userId, bookRideParamsDto);

			return Ok(_mapper.Map<BookRideResponse>(rideDto));
		}

		[HttpGet]
		public async Task<ActionResult<bool>> ValidateTicketCode(int userId, string ticketCode)
		{
			return Ok(await _ticketService.IsValid(userId, ticketCode));
		}
	}
}
