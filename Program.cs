using EmployeeApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog settings are read from appsettings.json (and appsettings.{Environment}.json)
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Starting EmployeeApi");

    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddScoped<EmployeeService>();

    var app = builder.Build();

    // Logs a summary line for every HTTP request
    app.UseSerilogRequestLogging();

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
