using HRCentral.API.Data;
using HRCentral.API.Models.Domain;
using HRCentral.API.Repositories.Interfaces;

namespace HRCentral.API.Repositories.Implementation;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<IEnumerable<Employee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddEmployeeAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEmployeeAsync(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmployeeAsync(int id)
    {
        throw new NotImplementedException();
    }
}