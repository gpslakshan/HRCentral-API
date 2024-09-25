using AutoMapper;
using HRCentral.API.DTOs;
using HRCentral.API.Entities;

namespace HRCentral.API.Mappings;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
    }
}