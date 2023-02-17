using AutoMapper;
using EMS_Api.EmployeeAggreagate;
using EMS_Service.EmployeeAggreagate;

namespace EMS_Api.CreateEmployeeAggreagate
{
    /// <summary>
    /// EmployeeProfile.
    /// </summary>
    public class  EmployeeProfile : Profile
    {
        /// <summary>
        /// Constructor of EmployeeProfile
        /// </summary>
        public EmployeeProfile()
        {
            CreateMap<EmployeeRequest, EmployeeDto>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<UpdateEmployeeRequest, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeResponse, EmployeeDto>().ReverseMap();
        }
    }
}