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

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid CompanyId, EmployeeCreationDto employeeCreation, bool TrackChanges)
        {
            //be sure about the company
            var company = await _repositoryManger.CompanyRepository.GetCompanyAsync(CompanyId,TrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(CompanyId);

            //pass to the repository layeres and save the Db
           var employee= _mapper.Map<Employee>(employeeCreation);
           _repositoryManger.EmployeeRepository.CreateEmployeeForCompany(CompanyId,employee);
           await _repositoryManger.saveAsync();

            //return to the controller
            var EmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return EmployeeDto;

        }

        public async Task DeleteEmployeeForCompanyAsync(Guid CompanyId, Guid id, bool TrackChanges)
        {
            var Company = await _repositoryManger.CompanyRepository.GetCompanyAsync(CompanyId, TrackChanges);
            if(Company is null)
                throw new CompanyNotFoundException(CompanyId);
            var employee= await _repositoryManger.EmployeeRepository.GetEmployeeAsync(CompanyId, id,TrackChanges);
            //if (employee is null)
            //    throw new EmployeeNotFoundException(id);
            _repositoryManger.EmployeeRepository.DeleteEmployeeForCompany(employee);
            await _repositoryManger.saveAsync();
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid Companyid, Guid Employeeid, bool TrackChanges)
        {
            //get the company
            var Company = await _repositoryManger.CompanyRepository.GetCompanyAsync(Companyid, TrackChanges);
            if(Company is null) throw new CompanyNotFoundException(Companyid);
            // get the employee 
            var employee= await _repositoryManger.EmployeeRepository.GetEmployeeAsync(Companyid,Employeeid, TrackChanges);
            if (employee is null) throw new EmployeeNotFoundException(Employeeid);
            var employeeDto= _mapper.Map<EmployeeDto>(employee);    
            return employeeDto;
        }

        public async Task<(EmployeeForUpdateDto employeePatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid CompanyId, Guid EmployeeId, bool CompTrackChanges, bool EmpTrackChanges)
        {
           var Company = await _repositoryManger.CompanyRepository.GetCompanyAsync(CompanyId,CompTrackChanges);
         if (Company is null)
               throw new CompanyNotFoundException(CompanyId);
           var Employee=await _repositoryManger.EmployeeRepository.GetEmployeeAsync(CompanyId,EmployeeId,EmpTrackChanges);    
         if (Employee is null)
              throw new EmployeeNotFoundException(EmployeeId);

         var employeeToPatch= _mapper.Map<EmployeeForUpdateDto>(Employee);
          return(employeeToPatch,Employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(Guid Companyid, bool TrackChanges)
        {
            var company =await _repositoryManger.CompanyRepository.GetCompanyAsync(Companyid, TrackChanges);
            if(company == null) { throw new CompanyNotFoundException(Companyid); }
            var employees =await _repositoryManger.EmployeeRepository.GetAllEmployeesAsync(TrackChanges); 
            var employeeDto= _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeeDto;
        }

        public async Task SavePatchChangesAsync(EmployeeForUpdateDto employeePatch, Employee employeeEntity)
        {
            _mapper.Map(employeePatch, employeeEntity);
            await _repositoryManger.saveAsync();
        }

        public async Task UpdateEmployeeAsync(Guid CompanyId, Guid EmployeeId, EmployeeForUpdateDto employeeForUpdateDto, bool CompTrackChanges, bool EmpTrackChanges)
        {
            var Company = await _repositoryManger.CompanyRepository.GetCompanyAsync(CompanyId, CompTrackChanges);
            if (Company is null)
                throw new CompanyNotFoundException(CompanyId);
            var Employee = await _repositoryManger.EmployeeRepository.GetEmployeeAsync(CompanyId, EmployeeId, EmpTrackChanges);
            if (Employee is null)
                throw new EmployeeNotFoundException(EmployeeId);
            _mapper.Map(employeeForUpdateDto, Employee);
            await _repositoryManger.saveAsync();
        }    
    }
}
