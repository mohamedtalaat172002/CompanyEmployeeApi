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
            try
            {
           var companies= _service.companyService.GetCompanies(Trackchanges:false);
                return Ok(companies);

            }
            catch(Exception ex) 
            {

                return StatusCode(500, $"A7A ht3ml 2ehh? dah internal Server Error{ex}");
            }
        }

    }
}
