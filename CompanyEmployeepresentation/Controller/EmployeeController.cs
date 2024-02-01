using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployeePresentation.Controller
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        private readonly IserviceManager _service;
        public EmployeeController(IserviceManager service)
        {
            _service = service;   
        }
        [HttpGet]
        public IActionResult GetEmployeeforSpecificCompany(Guid CompanyId) 
        {
           
            var emps= _service.employeeService.GetEmployees(CompanyId,TrackChanges:false);
            return Ok(emps);
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetEmployee(Guid CompanyId,Guid id)
        {
            var emp = _service.employeeService.GetEmployee(CompanyId, id, TrackChanges: false);
            return Ok(emp);
        }


    }
}
