﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingServiceApp.API.BookingService.Responses
{
	public class UserResponse
	{
		public int UserId { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
