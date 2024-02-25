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
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool Trackchanges);
        Task<Company> GetCompanyAsync(Guid id,bool TrackCahnges);
        void CreateCompany(Company company);
        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> Ids,bool TrackChanges);
        void DeleteCompnay(Company company);
    }
}
