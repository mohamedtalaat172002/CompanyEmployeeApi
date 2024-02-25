using Entities.Model;
using Shared.DataTranfere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IEmployeeServicecs
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(Guid Companyid, bool TrackChanges);
        Task<EmployeeDto> GetEmployeeAsync(Guid Companyid, Guid Employeeid, bool TrackChanges);
        Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid CompanyId,EmployeeCreationDto employeeDto,bool TrackChanges);
        Task DeleteEmployeeForCompanyAsync(Guid CompanyId, Guid id, bool TrackChanges); 
        Task UpdateEmployeeAsync(Guid CompanyId,Guid EmployeeId,EmployeeForUpdateDto employeeForUpdateDto,bool CompTrackChanges,bool EmpTrackChanges);

        Task<(EmployeeForUpdateDto employeePatch,Employee employeeEntity)> GetEmployeeForPatchAsync(Guid CompanyId,Guid EmployeeId,bool CompTrackChanges,bool EmpTrackChanges);
         
        Task SavePatchChangesAsync(EmployeeForUpdateDto employeePatch,Employee employeeEntity);
        
    }
}
