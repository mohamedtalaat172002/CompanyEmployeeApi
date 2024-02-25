﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Shared.DataTranfere;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public async Task< IActionResult> GetEmployeeforSpecificCompany(Guid CompanyId)
        {

            var emps = await _service.employeeService.GetAllEmployeesAsync(CompanyId, TrackChanges: false);
            return Ok(emps);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployee(Guid CompanyId, Guid id)
        {
            var emp =await _service.employeeService.GetEmployeeAsync(CompanyId, id, TrackChanges: false);
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeForComapny(Guid CompanyId, [FromBody] EmployeeCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");
             if(!ModelState.IsValid)
              return UnprocessableEntity(ModelState);    
            var EmployeeToReturn =await _service.employeeService.CreateEmployeeForCompanyAsync(CompanyId, employee, TrackChanges: false);
            return CreatedAtRoute("GetEmployeeForCompany", new { CompanyId, id = EmployeeToReturn.Id }, EmployeeToReturn);
        }

        [HttpDelete("{EmployeeId:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid CompanyId, Guid EmployeeId)
        {
            await _service.employeeService.DeleteEmployeeForCompanyAsync(CompanyId, EmployeeId, TrackChanges: false);
            return NoContent();
        }

        [HttpPut("id:guid")]
        public async Task< IActionResult> UpdateEmployee(Guid CompanyId,Guid id,EmployeeForUpdateDto employeeForUpdateDto)
        {
            if(employeeForUpdateDto is null)
              return BadRequest("the Employee u enterd is null");
            if(!ModelState.IsValid)
              return UnprocessableEntity(ModelState);
           
           await _service.employeeService.UpdateEmployeeAsync(CompanyId, id, employeeForUpdateDto, CompTrackChanges: false, EmpTrackChanges: true);
            return NoContent();
        }

        [HttpPatch("id:guid")]
        public async Task <IActionResult> PartiallyUpdateEmployee(Guid CompanyId,Guid EmployeeId,[FromBody] JsonPatchDocument<EmployeeForUpdateDto> jsonPatchDocument)
        {
        if(jsonPatchDocument is null)
           return BadRequest(" the patch document is null");

        var result=await _service.employeeService.GetEmployeeForPatchAsync(CompanyId,EmployeeId,CompTrackChanges:false,EmpTrackChanges:true);
        jsonPatchDocument.ApplyTo(result.employeePatch,ModelState);
        TryValidateModel(result.employeePatch);

        if (!ModelState.IsValid)
           return UnprocessableEntity(ModelState);

         await _service.employeeService.SavePatchChangesAsync(result.employeePatch,result.employeeEntity);
        return NoContent();
        }


    }
            
}
