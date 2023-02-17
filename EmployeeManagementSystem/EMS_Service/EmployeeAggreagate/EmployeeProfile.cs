using AutoMapper;
using EMS_Domain.EmployeeAggreagate;

namespace EMS_Service.EmployeeAggreagate
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}