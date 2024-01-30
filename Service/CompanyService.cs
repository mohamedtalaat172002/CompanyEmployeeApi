using AutoMapper;
using Contracts;

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
            try
            {
                var Companies = repositoryManger.CompanyRepository.GetCompanies(Trackchanges);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(Companies);
                return companiesDto;
            }
            catch (Exception ex)
            {
                loggerManager.errorLog($"Something went wrong in the { nameof(GetCompanies)}service method { ex} ");
                throw;
            }
        }
    }
}
