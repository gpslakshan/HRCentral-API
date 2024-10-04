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
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        var employeeDtos = MapToEmployeeDto(employees);
        return Ok(employeeDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById([FromRoute] int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        var employeeDto = MapToEmployeeDto(employee);
        return Ok(employeeDto);
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

        var employeeDto = MapToEmployeeDto(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDto.Id }, employeeDto);
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

        var employeeDto = MapToEmployeeDto(employee);
        return Ok(employeeDto);
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
    
    // Reusable method to map a single Employee to EmployeeDto
    private EmployeeDto MapToEmployeeDto(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            Position = employee.Position,
            Salary = employee.Salary
        };
    }

    // Reusable method to map a collection of Employees to EmployeeDtos
    private IEnumerable<EmployeeDto> MapToEmployeeDto(IEnumerable<Employee> employees)
    {
        return employees.Select(e => MapToEmployeeDto(e)).ToList();
    }
}