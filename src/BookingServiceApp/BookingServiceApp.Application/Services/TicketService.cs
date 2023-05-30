using AutoMapper;
using BookingServiceApp.Application.Exceptions;
using BookingServiceApp.Application.Helpers;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
using BookingServiceApp.Domain.Entities;
using BookingServiceApp.Domain.Repositories;
using BookingServiceApp.Domain.Specifications;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services
{
	public class TicketService : ITicketService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;
		public TicketService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_config = config;
		}


		public async Task<TicketDto> GenerateTicket(int userId, RideConfirmationDto rideConfirmationDto)
		{
			User user = await _unitOfWork.UserRepo.GetAsync(userId);
			if (user is null)
			{
				throw new UserNotFoundException(userId);
			}

			// Assemble ticket
			TicketDto ticketDto = _mapper.Map<TicketDto>(rideConfirmationDto);

			_mapper.Map(user, ticketDto);


			// Get ticket hash based on each property
			byte[] ticketHash = GetTicketHash(ticketDto);


			// Sign the ticket hash using DSA algorithm
			// Import private key
			DSACryptoServiceProvider dsaInstance = DSAImplementation.ImportKey(_config["TicketEncryptionPrivateKeyPath"]);

			// Create signature
			byte[] signature = DSAImplementation.CreateSignature(ticketHash, dsaInstance);

			ticketDto.TicketCode = Convert.ToBase64String(signature);

			return ticketDto;
		}

		public async Task<bool> IsValid(TicketDto ticketDto)
		{
			// Get ticket hash based on each property
			byte[] actualTicketHash = GetTicketHash(ticketDto);

			byte[] signature = Convert.FromBase64String(ticketDto.TicketCode);


			// Verify signature (that the ticket info [hash] hasn't changed) using DSA algorithm
			// Import public key
			DSACryptoServiceProvider dsaInstance = DSAImplementation.ImportKey(_config["TicketEncryptionPublicKeyPath"]);

			// Verify signature
			bool isValid = DSAImplementation.VerifySignature(actualTicketHash, signature, dsaInstance);

			return isValid;
		}

		byte[] GetTicketHash(TicketDto ticketDto)
		{
			// Transform properties to byte[] and assemble in one byte[]
			var concatenatedBytes = Encoding.Unicode.GetBytes(ticketDto.FirstName)
				.Concat(Encoding.Unicode.GetBytes(ticketDto.LastName))
				.Concat(BitConverter.GetBytes(ticketDto.BirthDate.Ticks))
				.Concat(BitConverter.GetBytes(ticketDto.DepartureTime.Ticks))
				.Concat(BitConverter.GetBytes(ticketDto.ArrivalTime.Ticks))
				.Concat(Encoding.Unicode.GetBytes(ticketDto.From))
				.Concat(Encoding.Unicode.GetBytes(ticketDto.To))
				.Concat(ticketDto.Seats.SelectMany(BitConverter.GetBytes))
				.ToArray();


			// Get ticket hash
			byte[] ticketHash = HashHelper.GetSHA256Hash(concatenatedBytes);

			return ticketHash;
		}
	}
}
