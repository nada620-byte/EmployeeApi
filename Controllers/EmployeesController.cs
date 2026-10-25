using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly ILogger<EmployeesController> _logger;
    private readonly EmployeeService _service;

    public EmployeesController(
        ILogger<EmployeesController> logger,
        EmployeeService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        _logger.LogInformation("Getting employee with ID {EmployeeId}", id);

        if (id <= 0)
        {
            _logger.LogWarning("Invalid employee ID: {EmployeeId}", id);
            return BadRequest();
        }

        return Ok(new { Id = id, Name = "Ahmed" });
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateEmployeeRequest request)
    {
        try
        {
            var id = _service.CreateEmployee(request.Name);
            return CreatedAtAction(nameof(GetById), new { id }, new { Id = id, request.Name });
        }
        catch (ArgumentException ex)
        {
            // Log once, with the exception object (keeps the stack trace)
            _logger.LogError(ex, "Failed to create employee {EmployeeName}", request.Name);
            return BadRequest(new { error = ex.Message });
        }
    }
}

public record CreateEmployeeRequest(string Name);
