using BookingServiceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services.Interfaces
{
	public interface ITicketService
	{
		Task<TicketDto> GenerateTicket(int userId, RideConfirmationDto rideDto);

		Task<bool> IsValid(TicketDto ticketDto);
	}
}
