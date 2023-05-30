using BookingServiceApp.Domain.Entities;
using BookingServiceApp.Domain.Repositories;
using BookingServiceApp.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Infrastructure.Repos
{
	public class UnitOfWork : IDisposable, IUnitOfWork
	{
		private BookingServiceContext _context;
		private IBaseRepo<User> _userRepo;
		private IBaseRepo<Ride> _rideRepo;
		private IBaseRepo<Seat> _seatRepo;
		public UnitOfWork(BookingServiceContext context)
		{
			_context = context;
		}

		public IBaseRepo<User> UserRepo
		{
			get
			{
				if (_userRepo is null)
				{
					_userRepo = new BaseRepo<User>(_context);
				}
				return _userRepo;
			}
		}

		public IBaseRepo<Ride> RideRepo
		{
			get
			{
				if (_rideRepo is null)
				{
					_rideRepo = new BaseRepo<Ride>(_context);
				}
				return _rideRepo;
			}
		}

		public IBaseRepo<Seat> SeatRepo
		{
			get
			{
				if (_seatRepo is null)
				{
					_seatRepo = new BaseRepo<Seat>(_context);
				}
				return _seatRepo;
			}
		}

		
		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}


		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
