using Contracts;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepositry:RepositoryBase<Employee>,IEmployeeRepository
    {
        public EmployeeRepositry(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            
        }

        public void CreateEmployeeForCompany(Guid CompanyId, Employee employee)
        {
            employee.CompanyId = CompanyId;
            Create(employee);
        }

        public void DeleteEmployeeForCompany(Employee employee)
        =>Delete(employee);

        public async Task <Employee> GetEmployeeAsync(Guid Companyid, Guid employeeid, bool TrackChanges)
        => await FindByCondition(x => x.CompanyId.Equals(Companyid) && x.Id.Equals(employeeid), TrackChanges).SingleOrDefaultAsync();

        public async Task<PagedList<Employee>> GetAllEmployeesAsync(EmployeeParameter employeeParameter, Guid CompanyId, bool TrackChanges)
        {
           var employees= await FindByCondition(x => x.CompanyId.Equals(CompanyId), TrackChanges).OrderBy(e => e.Name)
            .Skip((employeeParameter.PageNumber - 1) * employeeParameter.PageSize)
            .Take(employeeParameter.PageSize)
            .ToListAsync();
            return PagedList<Employee>
            .ToPagedList(employees, employeeParameter.PageNumber,employeeParameter.PageSize);
        }
    }
}
