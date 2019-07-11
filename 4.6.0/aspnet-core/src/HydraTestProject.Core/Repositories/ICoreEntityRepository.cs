using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using HydraTestProject.Models.Core;

namespace HydraTestProject.Repositories
{
    public interface ICoreEntityRepository : IRepository<CoreEntity, Guid>
    {
        Task<object> Test();
        Task<object> TestLambda();
    }
}
