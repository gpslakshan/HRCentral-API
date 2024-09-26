using HRCentral.API.Data;
using HRCentral.API.Models.Entities;
using HRCentral.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRCentral.API.Repositories.Implementation;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        
        if(employee == null)
        {
            throw new KeyNotFoundException($"Employee with id {id} was not found.");
        }
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}