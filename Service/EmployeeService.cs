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

        public EmployeeDto CreateEmployeeForCompany(Guid CompanyId, EmployeeCreationDto employeeCreation, bool TrackChanges)
        {
            //be sure about the company
            var company = _repositoryManger.CompanyRepository.GetCompany(CompanyId,TrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(CompanyId);

            //pass to the repository layeres and save the Db
           var employee= _mapper.Map<Employee>(employeeCreation);
            _repositoryManger.EmployeeRepository.CreateEmployeeForCompany(CompanyId,employee);
            _repositoryManger.save();

            //return to the controller
            var EmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return EmployeeDto;

        }

        public void DeleteEmployeeForCompany(Guid CompanyId, Guid id, bool TrackChanges)
        {
            var Company = _repositoryManger.CompanyRepository.GetCompany(CompanyId, TrackChanges);
            if(Company is null)
                throw new CompanyNotFoundException(CompanyId);
            var employee=_repositoryManger.EmployeeRepository.GetEmployee(CompanyId, id,TrackChanges);
            //if (employee is null)
            //    throw new EmployeeNotFoundException(id);
            _repositoryManger.EmployeeRepository.DeleteEmployeeForCompany(employee);
            _repositoryManger.save();
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

        public void UpdateEmployee(Guid CompanyId, Guid EmployeeId, EmployeeForUpdateDto employeeForUpdateDto, bool CompTrackChanges, bool EmpTrackChanges)
        {
            var Company = _repositoryManger.CompanyRepository.GetCompany(CompanyId,CompTrackChanges);
            if (Company is null) 
                throw new CompanyNotFoundException(CompanyId);
            var Employee= _repositoryManger.EmployeeRepository.GetEmployee(CompanyId,EmployeeId,EmpTrackChanges);
            if(Employee is null)
                throw new EmployeeNotFoundException(EmployeeId);
            _mapper.Map(employeeForUpdateDto, Employee);
            _repositoryManger.save();
                

        }
    }
}
