using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
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
         
           var companies= _service.companyService.GetCompanies(Trackchanges:false);
                return Ok(companies);    
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCompany(Guid id)
        {
            var Company = _service.companyService.GetCompany(id,TrackChanges:false);
            return Ok(Company);
        } 

    }
}
