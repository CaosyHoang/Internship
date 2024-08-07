using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Position, PositionDto>().ReverseMap();
        }
    }
}
