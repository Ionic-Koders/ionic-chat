﻿using despatch.Domain.Constants;
using despatch.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace despatch.Infrastructure.Extension
{
    public static class OptionsExtention
    {
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection smsOptons = configuration.GetSection("SMSOptions");
            var securityKey = configuration.GetSection("AuthOption:JwtKey").Value;
            services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
            services.Configure<SMSoptions>(smsOptons);
            services.AddAuthOptions(configuration, securityKey);
        }

        public static void AddAuthOptions(this IServiceCollection services, IConfiguration configuration, string key)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var tokenValidationParametr = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.Configure<AuthOption>(configuration.GetSection(typeof(AuthOption).Name));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParametr;
                x.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add(ExceptionConstant.TokenExpiredHeader, "true");
                        }
                        return Task.CompletedTask;
                    },
                };
            });
        }
    }
}
