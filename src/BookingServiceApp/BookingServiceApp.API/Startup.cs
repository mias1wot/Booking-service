using BookingServiceApp.API.AutoMapperProfiles;
using BookingServiceApp.API.Middlewares;
using BookingServiceApp.API.Validators.Ride;
using BookingServiceApp.Application.Services;
using BookingServiceApp.Application.Services.Interfaces;
using BookingServiceApp.Domain.Repositories;
using BookingServiceApp.Infrastructure.EF;
using BookingServiceApp.Infrastructure.Repos;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingServiceApp.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			ServicesConfiguration.Initialize(Configuration);

			services.AddControllers();

			services.AddHttpContextAccessor();

			services.AddAutoMapper(typeof(ApiProfile), typeof(ApplicationProfile));

			//FluentValidation.DependencyInjectionExtensions
			services.AddValidatorsFromAssemblyContaining<BookRideRequestValidator>();
			//services.AddFluentValidationAutoValidation(); // Available from version 9.3.0; Not recommended to use.
			//ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop; // Not available at this version.

			services.AddDbContext<BookingServiceContext>();

			services.AddHttpClient<IRouteApiService, RouteApiService>(client => {
				if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
				{
					client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("APPSETTINGS_ROUTE_SERVICE_BASE_URL"));
				}
				else
				{
					client.BaseAddress = new Uri(Configuration["RouteService:RouteApiServiceBaseUrl"]);
				}
			});

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddScoped<IRideService, RideService>();
			services.AddScoped<ITicketService, TicketService>();
			services.AddScoped<IUserService, UserService>();


			services.AddJwtAuthentication();

			services.AddSwagger();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "BookingServiceApp"));

			app.UseHttpsRedirection();

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
