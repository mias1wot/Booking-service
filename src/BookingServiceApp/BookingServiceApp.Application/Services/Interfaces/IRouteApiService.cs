using BookingServiceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services.Interfaces
{
	public interface IRouteApiService
	{
		Task<IEnumerable<RouteDto>> GetAvailableRoutesAsync(RouteSearchParamsDto routeSearchParamsDto);
		Task<RideConfirmationDto> BookRideAsync(BookRideParamsDto bookRideParamsDto);
	}
}
