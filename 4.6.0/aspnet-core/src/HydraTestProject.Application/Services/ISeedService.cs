using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using HydraTestProject.Models.Core;
using HydraTestProject.Models.Tabels;

namespace HydraTestProject.Services
{
    public interface ISeedService : IApplicationService
    {
        
        void CreateEntityType(int n);
        int GetRandomEntityTypeId();
        Task<TimeSpan> CreateEntity(int n);
        Task CreateValue();
        Guid GetRandomEntityGuid();
        Guid GetGuid(Guid input);
        void CreateProperties();
        int GetRandomEntityPropertyId();
        Task<object> Get();
        //List<object> GetSearchResult(QueryInfo query);

    }
}
