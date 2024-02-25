using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManger
    {
        public ICompanyRepository CompanyRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }

        Task saveAsync();
    }
}
