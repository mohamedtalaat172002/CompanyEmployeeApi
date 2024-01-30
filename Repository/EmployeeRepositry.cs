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
    }
}
