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
        CompanyDto CreateCompany(CompanyCreationDto company);
        IEnumerable<CompanyDto> GetAllByIdS(IEnumerable<Guid> Ids, bool TrackChaneges);
        (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyCreationDto> companies);
        void DeleteCompany(Guid CompanyId,bool trackChanges);
        void UpdateCompany(Guid CompanyId,CompanyForUpdateDto companyUpdateDto,bool TrackChanges );
    }
}
