using Contracts;
using Entities.Model;
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
        public Employee GetEmployee(Guid Companyid, Guid employeeid, bool TrackChanges)
        => FindByCondition(x => x.CompanyId.Equals(Companyid) && x.Id.Equals(employeeid), TrackChanges).SingleOrDefault();
        
        public IEnumerable<Employee> GetEmployees(Guid Companyid,bool TrackChanges)
        =>FindByCondition(x=>x.CompanyId.Equals(Companyid), TrackChanges).OrderBy(x=>x.Name).ToList();
    }
}
