using CompanyEmployeePresentation.ActionFilters;
using CompanyEmployeePresentation.ModelBinder;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared.DataTranfere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployeePresentation.Controller
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IserviceManager _service;
        public CompaniesController(IserviceManager service) => _service = service;

        [HttpGet]
        public async Task< IActionResult> GetAllCompanies()
        {

            var companies =await _service.companyService.GetAllCompaniesAsync(Trackchanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name = "GetCompanybyId")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var Company =await _service.companyService.GetCompanyAsync(id, TrackChanges: false);
            return Ok(Company);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationActionFilter))]
        public async Task<IActionResult> AddComapny([FromBody] CompanyCreationDto company)
        {
            var created = await _service.companyService.CreateCompanyAsync(company);
            return CreatedAtRoute("GetCompanybyId", new { id = created.Id }, created);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType =
         typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var Companies = await _service.companyService.GetCompaniesByIdsAsync(ids, TrackChaneges: false);
            return Ok(Companies);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateComapanyCollection([FromBody] IEnumerable<CompanyCreationDto> companies)
        {
            var companiesCollection =await _service.companyService.CreateCompanyCollectionAsync(companies);
            return CreatedAtRoute("CompanyCollection", new { companiesCollection.ids },
           companiesCollection.companies);
        }

        [HttpDelete("{CompanyId:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid CompanyId)
        {
           await _service.companyService.DeleteCompanyasync(CompanyId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{CompanyId:guid}")]
        [ServiceFilter(typeof(ValidationActionFilter))]
        public async Task<IActionResult>UpdteCompany(Guid CompanyId,CompanyForUpdateDto companyForUpdateDto) 
        {             
           await _service.companyService.UpdateCompanyAsync(CompanyId, companyForUpdateDto, TrackChanges: true);
            return NoContent();

        }
    }
}

