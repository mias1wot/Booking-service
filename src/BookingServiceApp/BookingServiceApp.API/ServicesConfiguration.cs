using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.API
{
	public static class ServicesConfiguration
	{
		private static IConfiguration _configuration;

		public static void Initialize(IConfiguration configuration)
		{
			_configuration = configuration;
		}


		public static void AddJwtAuthentication(this IServiceCollection services)
		{
			// Microsoft.AspNetCore.Authentication.JwtBearer v 3.1.32
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = _configuration["JWT:ValidIssuer"],
					ValidAudience = _configuration["JWT:ValidAudience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
				};
			});
		}

		public static void AddSwagger(this IServiceCollection services)
		{
			// You need to install Swashbuckle.AspNetCore
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("V1", new OpenApiInfo
				{
					Version = "V1",
					Title = "BookingServiceApp",
					Description = "BookingServiceApp Web Api"
				});
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Description = "Bearer Authentication with JWT Token",
					Type = SecuritySchemeType.Http
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Id = "Bearer",
								Type = ReferenceType.SecurityScheme
							}
						},
						new List<string>()
					}
				});
			});
		}
	}
}
