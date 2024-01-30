using AutoMapper;
using Contracts;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService:IEmployeeServicecs
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryManger _repositoryManger;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManger repositoryManger, ILoggerManager loggerManager,IMapper mapper)
        {
            _loggerManager = loggerManager;
            _repositoryManger = repositoryManger;
            _mapper = mapper;
        }

    }
}
