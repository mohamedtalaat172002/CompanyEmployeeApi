using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Model;
using ServiceContracts;
using Shared.DataTranfere;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        //CommonMethods
        public async Task CheckIfCompanyExisits(Guid id, bool TrackChanges)
        {
            var company = await _repositoryManger.CompanyRepository.GetCompanyAsync(id, TrackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(id);
            }    
        }

        public async Task<Employee> GetEmployeeAndCheckIfItExists(Guid Companyid, Guid Employeeid, bool TrackChanges)
        {
            var employee = await _repositoryManger.EmployeeRepository.GetEmployeeAsync(Companyid, Employeeid, TrackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(Employeeid);
            return employee;
        }

        //CrudMethods
        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid CompanyId, EmployeeCreationDto employeeCreation, bool TrackChanges)
        {
            //be sure about the company
             await CheckIfCompanyExisits(CompanyId, TrackChanges);

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
            await CheckIfCompanyExisits(CompanyId, TrackChanges);
            var employee= await GetEmployeeAndCheckIfItExists(CompanyId, id, TrackChanges);
            _repositoryManger.EmployeeRepository.DeleteEmployeeForCompany(employee);
            await _repositoryManger.saveAsync();
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid Companyid, Guid Employeeid, bool TrackChanges)
        {
            //get the company
            await CheckIfCompanyExisits(Companyid,TrackChanges);
            // get the employee 
            var employee = await GetEmployeeAndCheckIfItExists(Companyid, Employeeid, TrackChanges);
            var employeeDto= _mapper.Map<EmployeeDto>(employee);    
            return employeeDto;
        }

        public async Task<(EmployeeForUpdateDto employeePatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid CompanyId, Guid EmployeeId, bool CompTrackChanges, bool EmpTrackChanges)
        {
            await CheckIfCompanyExisits(CompanyId, CompTrackChanges);

            var Employee= await GetEmployeeAndCheckIfItExists(CompanyId, EmployeeId, EmpTrackChanges);

            var employeeToPatch= _mapper.Map<EmployeeForUpdateDto>(Employee);
          return(employeeToPatch,Employee);
        }

        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetAllEmployeesAsync(EmployeeParameter employeeParameter,Guid Companyid, bool TrackChanges)
        {
            await CheckIfCompanyExisits(Companyid, TrackChanges);

            var employeesWithMetaData = await _repositoryManger.EmployeeRepository
            .GetAllEmployeesAsync( employeeParameter, Companyid, TrackChanges);
            var employeesDto =_mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
            return (employees: employeesDto, metaData: employeesWithMetaData.MetaData);

        }

        public async Task SavePatchChangesAsync(EmployeeForUpdateDto employeePatch, Employee employeeEntity)
        {
            _mapper.Map(employeePatch, employeeEntity);
            await _repositoryManger.saveAsync();
        }

        public async Task UpdateEmployeeAsync(Guid CompanyId, Guid EmployeeId, EmployeeForUpdateDto employeeForUpdateDto, bool CompTrackChanges, bool EmpTrackChanges)
        {
            await CheckIfCompanyExisits(CompanyId, CompTrackChanges);

            var Employee = await GetEmployeeAndCheckIfItExists(CompanyId, EmployeeId, EmpTrackChanges);
            _mapper.Map(employeeForUpdateDto, Employee);
            await _repositoryManger.saveAsync();
        }    
    }
}
