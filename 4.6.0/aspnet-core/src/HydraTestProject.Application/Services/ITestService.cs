using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using HydraTestProject.DTO;
using HydraTestProject.Models.Core;
using HydraTestProject.Query;

namespace HydraTestProject.Services
{
    public interface ITestService : IApplicationService
    {
        List<CoreEntityDto> CreateSearchResult(QueryInfo query);
    }
}
