using HRCentral.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRCentral.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
}