using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTranfere
{
    public record CompanyCreationDto ( string Name, string Address, string Country,IEnumerable<EmployeeCreationDto>Employees );      
}
