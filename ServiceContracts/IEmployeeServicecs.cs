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
        public IEnumerable<EmployeeDto> GetEmployees(Guid Companyid, bool TrackChanges);
        public EmployeeDto GetEmployee(Guid Companyid, Guid Employeeid, bool TrackChanges);
    }
}
