using AutoMapper;
using HRCentral.API.DTOs;
using HRCentral.API.Entities;
using HRCentral.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HRCentral.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var allEmployees = await _employeeRepository.GetAllAsync();
        var response = _mapper.Map<IEnumerable<EmployeeDto>>(allEmployees);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }
        
        var response = _mapper.Map<EmployeeDto>(employee);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        var employee = _mapper.Map<Employee>(createEmployeeDto);
        await _employeeRepository.AddAsync(employee);
        var response = _mapper.Map<EmployeeDto>(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = _mapper.Map<Employee>(updateEmployeeDto);
        employee.Id = id;

        var updatedEmployee = await _employeeRepository.UpdateAsync(employee);

        if (updatedEmployee == null)
        {
            return NotFound();
        }
        
        var response = _mapper.Map<EmployeeDto>(updatedEmployee);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
    {
        var isDeleted = await _employeeRepository.DeleteAsync(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}