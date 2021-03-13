using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Infrastructure.Identity;
using Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interface;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class Setting
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddScoped<IUserManager, UserManagerServices>();
            // services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContextPool<ApplicationDbContext>(
                    opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddAuthorization(
                opt => opt.AddPolicy("RoleApplication",
                    policy => policy.RequireRole(Roles.ADMINISTRATOR, Roles.SALES, Roles.CLIENT)));

            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    var key = Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"]);

                    config.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.ContainsKey("access_token"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }

                            return Task.CompletedTask;
                        }
                    };

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = "",
                        ValidAudience = "",
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });

            // Identity register services
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
