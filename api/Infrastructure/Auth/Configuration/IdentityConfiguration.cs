using Infrastructure.Auth.Configuration.Option;
using Infrastructure.Auth.Context;
using Infrastructure.Auth.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Infrastructure.Auth.Model;

namespace Infrastructure.Auth.Configuration
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityWithJwt(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddDbContext<AuthContext>(o =>
                o.UseSqlServer(cfg.GetConnectionString("DefaultConnection")));

            services
                .AddIdentityCore<UserApplication>(opt =>
                {
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.User.RequireUniqueEmail = true;
                })
                .AddRoles<RoleApplication>()
                .AddEntityFrameworkStores<AuthContext>()
                .AddSignInManager();

            services.Configure<JwtOptions>(cfg.GetSection(JwtOptions.SectionName));
            var jwt = cfg.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("OnlyAdmins", p => p.RequireRole("admin"));
            });

            services.AddScoped<IJwtTokenService, JwtTokenService>();
            return services;
        }
    }
}
