namespace EmployeeApi.Services;

public class EmployeeService
{
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(ILogger<EmployeeService> logger)
    {
        _logger = logger;
    }

    public int CreateEmployee(string name)
    {
        // Structured logging: EmployeeName is stored as a separate property
        _logger.LogInformation("Creating employee {EmployeeName}", name);

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Employee name is required.", nameof(name));

        // Business logic (e.g. save to database) would go here
        var newId = Random.Shared.Next(1, 1000);

        _logger.LogInformation("Employee created successfully. EmployeeId: {EmployeeId}", newId);
        return newId;
    }
}
