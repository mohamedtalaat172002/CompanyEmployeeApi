using AutoMapper;
using Entities.Model;
using Shared.DataTranfere;

namespace CompanyEmployee
{
    public class MapingProfile:Profile
    {
        public MapingProfile()
        {
            CreateMap<Company, CompanyDto>();
                
            CreateMap<Employee, EmployeeDto>();

            CreateMap<CompanyCreationDto, Company>();

            CreateMap<EmployeeCreationDto, Employee>();

            CreateMap<EmployeeForUpdateDto,Employee>().ReverseMap();

            CreateMap <CompanyForUpdateDto,Company>();
            
        }
       
    }
}
