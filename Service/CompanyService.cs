using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Model;
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

        public CompanyDto CreateCompany(CompanyCreationDto company)
        {
            var companyDb= _mapper.Map<Company>(company);
            repositoryManger.CompanyRepository.CreateCompany(companyDb);
            repositoryManger.save();
            var companyTOreturn = _mapper.Map<CompanyDto>(companyDb);
            return companyTOreturn; 
        }

        public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyCreationDto> companiesDto)
        {
            if (companiesDto is null)
                throw new CompanyCollectionBadRequest();
            var companiesDb =_mapper.Map<IEnumerable<Company>>(companiesDto);  
            foreach (var company in companiesDb)
            {
                repositoryManger.CompanyRepository.CreateCompany(company);
               
            }
            repositoryManger.save();
            var CompaniescollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companiesDb);
            var Ids= string.Join(",", CompaniescollectionToReturn.Select(x=>x.Id));
            return(companies:CompaniescollectionToReturn, ids:Ids);
        }

        public void DeleteCompany(Guid CompanyId, bool trackChanges)
        {
            var Company = repositoryManger.CompanyRepository.GetCompany(CompanyId, trackChanges);
            if (Company is null)
                throw new CompanyNotFoundException(CompanyId);
            repositoryManger.CompanyRepository.DeleteCompnay(Company);
            repositoryManger.save();
        }

        public IEnumerable<CompanyDto> GetAllByIdS(IEnumerable<Guid> Ids, bool TrackChaneges)
        {
            if (Ids is null)
                throw new IdParametersBadRequestException();
            var Companies = repositoryManger.CompanyRepository.GetByIds(Ids, TrackChaneges);
            if (Ids.Count() != Companies.Count())
                throw new CollectionByIdsBadRequestException();
            var CompaniesDto= _mapper.Map<IEnumerable<CompanyDto>>(Companies);
            return CompaniesDto;
           
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

        public void UpdateCompany(Guid CompanyId, CompanyForUpdateDto companyUpdateDto, bool TrackChanges)
        {
            var Company = repositoryManger.CompanyRepository.GetCompany(CompanyId, TrackChanges);
            if (Company is null)
                throw new CompanyNotFoundException(CompanyId);
            _mapper.Map(companyUpdateDto,Company);
            repositoryManger.save();
        }
    }
}
