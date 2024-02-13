using AutoMapper;
using Contracts;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManger : IserviceManager
    {
        private readonly Lazy<IcompanyService> _companyService;
        private readonly Lazy<IEmployeeServicecs> _employeeServicecs;


        public ServiceManger(IRepositoryManger repositoryManger,ILoggerManager loggerManager,IMapper mapper)
        {
            _companyService = new Lazy<IcompanyService>(() => new
                CompanyService(repositoryManger, loggerManager,mapper));

            _employeeServicecs = new Lazy<IEmployeeServicecs>(() => new
               EmployeeService(repositoryManger, loggerManager,mapper));
        }

        public IcompanyService companyService => _companyService.Value;

        public IEmployeeServicecs employeeService => _employeeServicecs.Value;
    }
}
