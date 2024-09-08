using ChatGPTClone.Infrastructure;
using ChatGPTClone.Application;
using ChatGPTClone.WebApi;
using ChatGPTClone.WebApi.Filters;
using Serilog;
using Microsoft.Extensions.Options;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt",rollingInterval:RollingInterval.Day)
    .MinimumLevel.Fatal()
    .CreateLogger();
try
{
    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog();
    // Add services to the container.

    builder.Services.AddControllers(opt =>
    {
        opt.Filters.Add<GlobalExceptionFilter>();
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddApplicaton();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddWebApi(builder.Configuration,builder.Environment);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    var requestLocalizationOptions=app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
    app.UseRequestLocalization(requestLocalizationOptions);
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{

    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();

}