using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using HydraTestProject.Models.Core;

namespace HydraTestProject.Services
{
    public class SeedService : HydraTestProjectAppServiceBase, ISeedService
    {
        private readonly IRepository<CoreEntityType> _repository;

        public SeedService(IRepository<CoreEntityType> repository)
        {
            _repository = repository;
        }

        public void Create(int n)
        {
            for()
        }
    }
}
