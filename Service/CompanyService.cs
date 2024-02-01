using AutoMapper;
using Contracts;
using Entities.Exceptions;
using ServiceContracts;
using Shared.DataTranfere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CompanyService:IcompanyService
    {
        private readonly IRepositoryManger repositoryManger;
        private readonly ILoggerManager loggerManager;
        private readonly IMapper _mapper;    

        public CompanyService(IRepositoryManger repositoryManger, ILoggerManager loggerManager,IMapper mapper)
        {
            this.repositoryManger = repositoryManger;
            this.loggerManager = loggerManager;
            this._mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetCompanies(bool Trackchanges)
        {
            
                var Companies = repositoryManger.CompanyRepository.GetCompanies(Trackchanges);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(Companies);
                return companiesDto;           
        }

        public CompanyDto GetCompany(Guid Companyid, bool TrackChanges)
        {
            var company = repositoryManger.CompanyRepository.GetCompany(Companyid,TrackChanges);
            if(company == null)
            {
                throw new CompanyNotFoundException(Companyid);
            }
            var CompanyDto = _mapper.Map<CompanyDto>(company);
            return CompanyDto;
        }
    }
}
