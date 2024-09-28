using System.ComponentModel.DataAnnotations;

namespace HRCentral.API.Models.DTOs;

public class CreateEmployeeDto
{
    [Required(ErrorMessage = "First Name is required")]
    [Length(1, 100)]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last Name is required")]
    [Length(1, 100)]
    public required string LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Length(1, 100)]
    public required string Email { get; set; }
    
    [Length(0, 10)]
    public string? Phone { get; set; }
    
    [Required(ErrorMessage = "Position is required")]
    [Length(1, 50)]
    public required string Position { get; set; }
    
    [Required(ErrorMessage = "Salary is required")]
    [Range(0, 1_000_000)]
    public decimal Salary { get; set; }
}