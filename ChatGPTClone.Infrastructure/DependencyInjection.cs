using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Domain.Settings;
using ChatGPTClone.Infrastructure.Identity;
using ChatGPTClone.Infrastructure.Services;
using ChatGPTClone.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Extensions;
using Resend;

namespace ChatGPTClone.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Veritabanı bağlantı dizesini yapılandırmadan alır
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // ApplicationDbContext'i PostgreSQL ile kullanmak üzere yapılandırır
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));

            // IApplicationDbContext'i ApplicationDbContext ile eşler
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddOpenAIService(settings => settings.ApiKey = configuration.GetSection("OpenAiApiKey").Value!);
            // JWT ayarlarını yapılandırır
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddScoped<IJwtService, JwtManager>();

            services.AddScoped<IIdentityService, IdentityManager>();

            services.AddScoped<IEmailService, ResendEmailManager>();

            services.AddScoped<IOpenAiService, OpenAiManager>();

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Resend
            services.AddOptions();

            services.AddHttpClient<ResendClient>();

            services.Configure<ResendClientOptions>(o => o.ApiToken = configuration.GetSection("ResendApiKey").Value!);

            services.AddTransient<IResend, ResendClient>();

            return services;
        }
    }
}
