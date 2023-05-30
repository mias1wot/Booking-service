using BookingServiceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services.Interfaces
{
	public interface IRideService
	{
		Task<IEnumerable<RideDto>> GetUserRidesAsync(int userId);
		Task<IEnumerable<RouteDto>> GetAvailableRoutesAsync(RouteSearchParamsDto routeSearchParamsDto);
		Task<TicketDto> BookRideAsync(int userId, BookRideParamsDto bookRideParamsDto);
	}
}
