using Entities.Model;
using Shared.DataTranfere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies(bool Trackchanges);
        public Company GetCompany(Guid id,bool TrackCahnges);
        public void CreateCompany(Company company);
        IEnumerable<Company> GetByIds(IEnumerable<Guid> Ids,bool TrackChanges);
        void DeleteCompnay(Company company);

    }
}
