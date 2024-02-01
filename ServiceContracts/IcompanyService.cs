using Shared.DataTranfere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IcompanyService
    {
        IEnumerable<CompanyDto> GetCompanies(bool Trackchanges);
         CompanyDto GetCompany(Guid id, bool TrackChanges);
    }
}
