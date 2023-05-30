using BookingServiceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Domain.Repositories
{
	public interface IUnitOfWork
	{
		IBaseRepo<User> UserRepo { get; }
		IBaseRepo<Ride> RideRepo { get; }
		IBaseRepo<Seat> SeatRepo { get; }
		Task SaveAsync();
	}
}
