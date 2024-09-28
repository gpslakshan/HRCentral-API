using HRCentral.API.Models.DTOs;
using HRCentral.API.Models.Entities;
using HRCentral.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRCentral.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public EmployeesController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var allEmployees = await _employeeRepository.GetAllEmployeesAsync();
        return Ok(allEmployees);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeDto createEmployeeDto)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }
        
        var employee = new Employee
        {
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            Email = createEmployeeDto.Email,
            Phone = createEmployeeDto.Phone,
            Position = createEmployeeDto.Position,
            Salary = createEmployeeDto.Salary
        };

        await _employeeRepository.AddEmployeeAsync(employee);
        
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }
        
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        employee.FirstName = updateEmployeeDto.FirstName;
        employee.LastName = updateEmployeeDto.LastName;
        employee.Email = updateEmployeeDto.Email;
        employee.Phone = updateEmployeeDto.Phone;
        employee.Position = updateEmployeeDto.Position;
        employee.Salary = updateEmployeeDto.Salary;

        await _employeeRepository.UpdateEmployeeAsync(employee);

        return Ok(employee);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        await _employeeRepository.DeleteEmployeeAsync(id);

        return Ok();
    }
}