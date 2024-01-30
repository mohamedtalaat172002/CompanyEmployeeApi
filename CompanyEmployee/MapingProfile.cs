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
            
        }
       
    }
}
