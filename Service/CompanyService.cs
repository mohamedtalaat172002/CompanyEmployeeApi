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

        public async Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto company)
        {
            var companyDb= _mapper.Map<Company>(company);
            repositoryManger.CompanyRepository.CreateCompany(companyDb);
            await repositoryManger.saveAsync();
            var companyTOreturn = _mapper.Map<CompanyDto>(companyDb);
            return companyTOreturn; 
        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyCreationDto> companiesDto)
        {
            if (companiesDto is null)
                throw new CompanyCollectionBadRequest();
            var companiesDb =_mapper.Map<IEnumerable<Company>>(companiesDto);  
            foreach (var company in companiesDb)
            {
               repositoryManger.CompanyRepository.CreateCompany(company);    
            }
            await repositoryManger.saveAsync();
            var CompaniescollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companiesDb);
            var Ids= string.Join(",", CompaniescollectionToReturn.Select(x=>x.Id));
            return(companies:CompaniescollectionToReturn, ids:Ids);
        }

        public async Task DeleteCompanyasync(Guid CompanyId, bool trackChanges)
        {
            var Company = await repositoryManger.CompanyRepository.GetCompanyAsync(CompanyId, trackChanges);
            if (Company is null)
                throw new CompanyNotFoundException(CompanyId);
            repositoryManger.CompanyRepository.DeleteCompnay(Company);
            await repositoryManger.saveAsync();
        }

        public async Task<IEnumerable<CompanyDto>> GetCompaniesByIdsAsync(IEnumerable<Guid> Ids, bool TrackChaneges)
        {
            if (Ids is null)
                throw new IdParametersBadRequestException();
            var Companies = await repositoryManger.CompanyRepository.GetByIdsAsync(Ids, TrackChaneges);
            if (Ids.Count() != Companies.Count())
                throw new CollectionByIdsBadRequestException();
            var CompaniesDto= _mapper.Map<IEnumerable<CompanyDto>>(Companies);
            return CompaniesDto;
           
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool Trackchanges)
        {
            
                var Companies = await repositoryManger.CompanyRepository.GetAllCompaniesAsync(Trackchanges);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(Companies);
                return companiesDto;           
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid Companyid, bool TrackChanges)
        {
            var company =await repositoryManger.CompanyRepository.GetCompanyAsync(Companyid,TrackChanges);
            if(company == null)
            {
                throw new CompanyNotFoundException(Companyid);
            }
            var CompanyDto = _mapper.Map<CompanyDto>(company);
            return CompanyDto;
        }

        public async Task UpdateCompanyAsync(Guid CompanyId, CompanyForUpdateDto companyUpdateDto, bool TrackChanges)
        {
            var Company = await repositoryManger.CompanyRepository.GetCompanyAsync(CompanyId, TrackChanges);
            if (Company is null)
                throw new CompanyNotFoundException(CompanyId);
            _mapper.Map(companyUpdateDto,Company);
           await repositoryManger.saveAsync();
        }
    }
}
