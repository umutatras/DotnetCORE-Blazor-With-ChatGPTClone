using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Domain.Settings;
using ChatGPTClone.Infrastructure.Identity;
using ChatGPTClone.Infrastructure.Services;
using ChatGPTClone.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace ChatGPTClone.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            //services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            ConfigureJwtSettings(services,configuration);
            services.AddScoped<IJwtService, JwtManager>();
            services.AddScoped<IIdentityService, IdentityManager>();
            services.AddScoped<IEmailService, ResendEmailManager>();
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit=false;
                options.Password.RequireLowercase=false;
                options.Password.RequireUppercase=false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            services.AddOptions();
            services.AddHttpClient<ResendClient>();
            services.Configure<ResendClientOptions>(o =>
            {
                o.ApiToken = configuration.GetSection("ResendApiKey").Value!;
            });
            services.AddTransient<IResend, ResendClient>();
      
            return services;
        }
       // JWT ayarlarını yapılandıran özel metod
        private static void ConfigureJwtSettings(IServiceCollection services, IConfiguration configuration)
        {
            // Yapılandırmadan JWT ayarları bölümünü alır
            var jwtSettingsSection = configuration.GetSection("JwtSettings");

            // Eğer JWT ayarları yapılandırmada mevcutsa, bu ayarları kullanır
            if (jwtSettingsSection.Exists())
            {
                services.Configure<JwtSettings>(jwtSettingsSection);
            }
            // Aksi takdirde, varsayılan değerlerle JWT ayarlarını yapılandırır
            else
            {
                services.Configure<JwtSettings>(options =>
                {
                    options.SecretKey = "default-secret-key-for-development-only";
                    options.AccessTokenExpiration = TimeSpan.FromMinutes(30);
                    options.RefreshTokenExpiration = TimeSpan.FromDays(7);
                    options.Issuer = "ChatGPTClone";
                    options.Audience = "ChatGPTClone";
                });
            }
        }
    }
}
