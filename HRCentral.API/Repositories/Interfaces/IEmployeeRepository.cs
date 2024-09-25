using HRCentral.API.Models.Entities;

namespace HRCentral.API.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task AddEmployeeAsync(Employee employee);
    Task SaveChangesAsync();
    Task DeleteEmployeeAsync(Employee employee);
}