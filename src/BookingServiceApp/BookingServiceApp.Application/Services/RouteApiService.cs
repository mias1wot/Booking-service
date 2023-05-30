using BookingServiceApp.Application.Exceptions;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Application.Services
{
	public class RouteApiService : IRouteApiService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;
		public RouteApiService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
		}

		public async Task<IEnumerable<RouteDto>> GetAvailableRoutesAsync(RouteSearchParamsDto routeSearchParamsDto)
		{
			string json = JsonConvert.SerializeObject(routeSearchParamsDto);
			var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(_configuration["RouteService:GetAvailableRoutesUri"], requestContent);

			if (!response.IsSuccessStatusCode)
			{
				throw new RouteServiceException(response.ReasonPhrase, response.StatusCode);
			}

			string content = await response.Content.ReadAsStringAsync();

			var routeDtos = JsonConvert.DeserializeObject<IEnumerable<RouteDto>>(content);

			return routeDtos;
		}

		public async Task<RideConfirmationDto> BookRideAsync(BookRideParamsDto bookRideParamsDto)
		{
			string json = JsonConvert.SerializeObject(bookRideParamsDto);
			var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(_configuration["RouteService:BookRideUri"], requestContent);

			if (!response.IsSuccessStatusCode)
			{
				throw new RouteServiceException(response.ReasonPhrase, response.StatusCode);
			}

			string content = await response.Content.ReadAsStringAsync();

			var rideConfirmationDto = JsonConvert.DeserializeObject<RideConfirmationDto>(content);

			if (!rideConfirmationDto.IsSuccess)
			{
				throw new RideConfirmationException(rideConfirmationDto.Errors);
			}

			return rideConfirmationDto;
		}
	}
}
