namespace HRCentral.API.Models.DTOs;

public class CreateEmployeeDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string Position { get; set; }
    public decimal Salary { get; set; }
}