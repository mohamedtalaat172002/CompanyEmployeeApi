using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManger : IRepositoryManger
    {

        private ApplicationDbContext _context;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public RepositoryManger( ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new
            ComapnyReopsitory(applicationDbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() =>
            new EmployeeRepositry(applicationDbContext));
        }
        public ICompanyRepository CompanyRepository => _companyRepository.Value;

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public void save()=>_context.SaveChanges();
       
    }
}
