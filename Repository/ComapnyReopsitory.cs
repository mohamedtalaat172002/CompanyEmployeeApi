using Contracts;
using Entities.Model;
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

        public IEnumerable<Company> GetCompanies(bool Trackchanges)
        =>FindAll(Trackchanges).OrderBy(o=>o.Name).ToList();

        public Company GetCompany(Guid id, bool TrackCahnges)
        => FindByCondition(x => x.Id.Equals(id), TrackCahnges).SingleOrDefault();
      
        public void CreateCompany(Company company)=>
          Create(company);

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> Ids, bool TrackChanges)
          =>  FindByCondition(x => Ids.Contains(x.Id), TrackChanges).ToList();

        public void DeleteCompnay(Company company)
        => Delete(company); 
    }
}
