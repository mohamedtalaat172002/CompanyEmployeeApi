using Contracts;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Shared.DataTranfere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ComapnyReopsitory:RepositoryBase<Company>,ICompanyRepository
    {
        public ComapnyReopsitory(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {  

        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) =>
        await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefaultAsync();

        public void CreateCompany(Company company) => Create(company);

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool TrackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), TrackChanges).ToListAsync();

        public void DeleteCompnay(Company company) => Delete(company);

    }
}
