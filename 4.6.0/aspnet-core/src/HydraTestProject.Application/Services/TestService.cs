using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Abp.Domain.Repositories;
using HydraTestProject.DTO;
using HydraTestProject.Models.Core;
using HydraTestProject.Query;
using Microsoft.EntityFrameworkCore;

namespace HydraTestProject.Services
{
    public class TestService : HydraTestProjectAppServiceBase, ITestService
    {
        private readonly IRepository<CoreEntity, Guid> _repositoryEntity;


        [HttpPost]
        public List<CoreEntityDto> CreateSearchResult([FromBody]QueryInfo query)
        {
            var allDevices = _repositoryEntity.GetAll()/*.Include(x => x.EntityType)*/;

            var result = query.GetQuery(query, allDevices).ToList();

            var dtoResult = ObjectMapper.Map<List<CoreEntityDto>>(result);

            return dtoResult;
        }
    }
}
