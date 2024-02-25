using Contracts;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
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
        
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool TrackChanges)
        =>await FindAll(TrackChanges).OrderBy(x=>x.Name).ToListAsync();
    }
}
