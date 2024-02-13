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
        public IActionResult GetAllCompanies()
        {

            var companies = _service.companyService.GetCompanies(Trackchanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name = "GetCompanybyId")]
        public IActionResult GetCompany(Guid id)
        {
            var Company = _service.companyService.GetCompany(id, TrackChanges: false);
            return Ok(Company);
        }

        [HttpPost]
        public IActionResult AddComapny([FromBody] CompanyCreationDto company)
        {
            if (company is null)
                return BadRequest("the company is null");
            var created = _service.companyService.CreateCompany(company);
            return CreatedAtRoute("GetCompanybyId", new { id = created.Id }, created);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public IActionResult GetCompanyCollection([ModelBinder(BinderType =
         typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var Companies = _service.companyService.GetAllByIdS(ids, TrackChaneges: false);
            return Ok(Companies);
        }

        [HttpPost("collection")]
        public IActionResult CreateComapanyCollection([FromBody] IEnumerable<CompanyCreationDto> companies)
        {
            var companiesCollection = _service.companyService.CreateCompanyCollection(companies);
            return CreatedAtRoute("CompanyCollection", new { companiesCollection.ids },
           companiesCollection.companies);
        }

        [HttpDelete("{CompanyId:guid}")]
        public IActionResult DeleteCompany(Guid CompanyId)
        {
            _service.companyService.DeleteCompany(CompanyId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{CompanyId:guid}")]
        public IActionResult UpdteCompany(Guid CompanyId,CompanyForUpdateDto companyForUpdateDto) 
        {
            _service.companyService.UpdateCompany(CompanyId, companyForUpdateDto, TrackChanges: true);
            return NoContent();

        }
    }
}

