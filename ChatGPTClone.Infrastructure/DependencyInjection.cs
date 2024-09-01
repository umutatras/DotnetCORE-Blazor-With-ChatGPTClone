﻿using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Domain.Settings;
using ChatGPTClone.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatGPTClone.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            //ConfigureJwtSettings(services,configuration);
            return services;
        }
        // JWT ayarlarını yapılandıran özel metod
        //private static void ConfigureJwtSettings(IServiceCollection services, IConfiguration configuration)
        //{
        //    // Yapılandırmadan JWT ayarları bölümünü alır
        //    var jwtSettingsSection = configuration.GetSection("JwtSettings");

        //    // Eğer JWT ayarları yapılandırmada mevcutsa, bu ayarları kullanır
        //    if (jwtSettingsSection.Exists())
        //    {
        //        services.Configure<JwtSettings>(jwtSettingsSection);
        //    }
        //    // Aksi takdirde, varsayılan değerlerle JWT ayarlarını yapılandırır
        //    else
        //    {
        //        services.Configure<JwtSettings>(options =>
        //        {
        //            options.SecretKey = "default-secret-key-for-development-only";
        //            options.AccessTokenExpiration = TimeSpan.FromMinutes(30);
        //            options.RefreshTokenExpiration = TimeSpan.FromDays(7);
        //            options.Issuer = "ChatGPTClone";
        //            options.Audience = "ChatGPTClone";
        //        });
        //    }
        //}
    }
}
