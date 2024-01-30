﻿using Entities.Model;
using Shared.DataTranfere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies(bool Trackchanges);
    }
}
