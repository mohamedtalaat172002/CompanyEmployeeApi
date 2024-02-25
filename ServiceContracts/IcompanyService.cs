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
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool Trackchanges);
        Task<CompanyDto> GetCompanyAsync(Guid id, bool TrackChanges);
        Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto company);
        Task<IEnumerable<CompanyDto>> GetCompaniesByIdsAsync(IEnumerable<Guid> Ids, bool TrackChaneges);
        Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyCreationDto> companies);
        Task DeleteCompanyasync(Guid CompanyId,bool trackChanges);
        Task UpdateCompanyAsync(Guid CompanyId,CompanyForUpdateDto companyUpdateDto,bool TrackChanges );
    }
}
