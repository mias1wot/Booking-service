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
	public class UserController : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<UserResponse>> GetUser()
		{
			return await Task.FromResult(NoContent());
		}

		[HttpPost]
		public async Task<ActionResult<UserResponse>> CreateUser(CreateUserRequest request)
		{
			return await Task.FromResult(NoContent());
			//return CreatedAtAction("GetUser", new {id = })
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUser(UpdateUserRequest request)
		{
			return await Task.FromResult(NoContent());
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteUser(int id)
		{
			return await Task.FromResult(NoContent());
		}
	}
}
