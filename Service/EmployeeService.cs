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
    internal sealed class EmployeeService:IEmployeeServicecs
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryManger _repositoryManger;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManger repositoryManger, ILoggerManager loggerManager,IMapper mapper)
        {
            _loggerManager = loggerManager;
            _repositoryManger = repositoryManger;
            _mapper = mapper;
        }

        public EmployeeDto GetEmployee(Guid Companyid, Guid Employeeid, bool TrackChanges)
        {
            //get the company
            var Company = _repositoryManger.CompanyRepository.GetCompany(Companyid, TrackChanges);
            if(Company is null) throw new CompanyNotFoundException(Companyid);
            // get the employee 
            var employee= _repositoryManger.EmployeeRepository.GetEmployee(Companyid,Employeeid, TrackChanges);
            if (employee is null) throw new EmployeeNotFoundException(Employeeid);
            var employeeDto= _mapper.Map<EmployeeDto>(employee);    
            return employeeDto;
            

        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid Companyid, bool TrackChanges)
        {
            var company = _repositoryManger.CompanyRepository.GetCompany(Companyid, TrackChanges);
            if(company == null) { throw new CompanyNotFoundException(Companyid); }
            var employees = _repositoryManger.EmployeeRepository.GetEmployees(Companyid, TrackChanges); 
            var employeeDto= _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeeDto;
        }
    }
}
