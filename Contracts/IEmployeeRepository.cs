﻿using Entities.Model;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task <PagedList<Employee>> GetAllEmployeesAsync(EmployeeParameter employeeParameter,Guid CompanyId,bool TrackChanges);
        Task<Employee> GetEmployeeAsync(Guid Companyid,Guid employeeid,bool TrackChanges);
        public void CreateEmployeeForCompany(Guid CompanyId,Employee employee);
        public void DeleteEmployeeForCompany(Employee employee);
    }
}
