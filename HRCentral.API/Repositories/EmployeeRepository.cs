using HRCentral.API.Data;
using HRCentral.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRCentral.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> UpdateAsync(Employee employee)
    {
        var employeeInDb = await _context.Employees.FindAsync(employee.Id);
        
        if (employeeInDb == null)
        {
            return null;
        }

        employeeInDb.FirstName = employee.FirstName;
        employeeInDb.LastName = employee.LastName;
        employeeInDb.Email = employee.Email;
        employeeInDb.Phone = employee.Phone;
        employeeInDb.Position = employee.Position;
        
        await _context.SaveChangesAsync();
        return employeeInDb;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        
        if (employee == null)
        {
            return false;
        }
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
}