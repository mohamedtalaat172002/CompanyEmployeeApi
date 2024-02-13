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
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IserviceManager _service;
        public EmployeeController(IserviceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetEmployeeforSpecificCompany(Guid CompanyId)
        {

            var emps = _service.employeeService.GetEmployees(CompanyId, TrackChanges: false);
            return Ok(emps);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployee(Guid CompanyId, Guid id)
        {
            var emp = _service.employeeService.GetEmployee(CompanyId, id, TrackChanges: false);
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult AddEmployeeForComapny(Guid CompanyId, [FromBody] EmployeeCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");
            var EmployeeToReturn = _service.employeeService.CreateEmployeeForCompany(CompanyId, employee, TrackChanges: false);
            return CreatedAtRoute("GetEmployeeForCompany", new { CompanyId, id = EmployeeToReturn.Id }, EmployeeToReturn);
        }

        [HttpDelete("{EmployeeId:guid}")]
        public IActionResult DeleteEmployee(Guid CompanyId, Guid EmployeeId)
        {
            _service.employeeService.DeleteEmployeeForCompany(CompanyId, EmployeeId, TrackChanges: false);
            return NoContent();
        }

        [HttpPut("id:guid")]
        public IActionResult UpdateEmployee(Guid CompanyId,Guid id,EmployeeForUpdateDto employeeForUpdateDto)
        {
            _service.employeeService.UpdateEmployee(CompanyId, id, employeeForUpdateDto, CompTrackChanges: false, EmpTrackChanges: true);
            return NoContent();
        }
    }
            
}
