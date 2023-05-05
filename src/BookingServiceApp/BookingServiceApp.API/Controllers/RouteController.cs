using BookingServiceApp.API.BookingService.Requests;
using BookingServiceApp.API.BookingService.Responses;
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
	public class RouteController : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<AvailableRoutesResponse>> GetAvailableRoutes()
		{
			return await Task.FromResult(NoContent());
		}

		[HttpPost]
		public async Task<ActionResult<BookRouteResponse>> BookRoute(BookRouteRequest request)
		{
			return await Task.FromResult(NoContent());
		}

		[HttpPost]
		public async Task<ActionResult<ValidateTicketCodeResponse>> ValidateTicketCode(ValidateTicketCodeRequest request)
		{
			return await Task.FromResult(NoContent());
		}
	}
}
