using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using HydraTestProject.Models;
using HydraTestProject.Models.Core;
using HydraTestProject.Models.Tabels;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HydraTestProject.Services
{
    public class SeedService : HydraTestProjectAppServiceBase //ISeedService
    {
        private readonly IRepository<CoreEntityType> _repository;
        private readonly IRepository<TableA> _repositoryA;
        private readonly IRepository<CoreEntityTypeProperty> _repositoryProperty;


        public SeedService(IRepository<CoreEntityType> repository, IRepository<TableA> repositoryA, IRepository<CoreEntityTypeProperty> repositoryProperty)
        {
            _repository = repository;
            _repositoryA = repositoryA;
            _repositoryProperty = repositoryProperty;
        }


        public void CreateEntityType(int n)
        {
            var jsonString = File.ReadAllText("MajaMegi.json");
            var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);


            for (var i = 0; i < n; i++)
            {
                Random r = new Random();
                int index = r.Next(1, 1000);

                var entityType = new CoreEntityType()
                {
                    Name = listA[index].Name,
                    Path = listA[index].Path,
                    Description = listA[index].Description,
                    IconName = listA[index].Name,
                    InsertIpAddress = listA[index].IpAddress,
                    InsertTime = listA[index].Date,
                    InsertUserId = listA[index].Id,
                    IsCustom = listA[index].Bool,
                    IsActive = listA[index].Bool,
                    LastUpdateIpAddress = listA[index].IpAddress,
                    LastUpdateTime = listA[index].Date,
                    ParentEntityTypeId = null,
                    LastUpdateUserId = listA[index].Id,
                    Level = listA[index].Id,
                    Order = listA[index].Id

                };

                _repository.Insert(entityType);

            }

        }


        public void CreateEntityTypeProperty(int n)
        {
            var jsonString = File.ReadAllText("MajaMegi.json");
            var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);


            for (var i = 0; i < n; i++)
            {
                Random r = new Random();
                int index = r.Next(1, 1000);
                int index1 = r.Next(1, 1000);


                var entityTypeProperty = new CoreEntityTypeProperty()
                {
                    Name = listA[index].Name,

                    DbPrecision = listA[index].Description,
                    DefaultValue = listA[index].Path,
                    Description = listA[index].Description,
                    DbType = listA[index].Name,
                    IsProtected = listA[index].Bool,
                    IsRequired = listA[index].Bool,
                    EntityTypeId = GetId(),
                    InsertIpAddress = listA[index].IpAddress,
                    InsertTime = listA[index].Date,
                    IsCustom = listA[index].Bool,
                    LastUpdateIpAddress = listA[index].IpAddress,
                    LastUpdateTime = listA[index].Date,
                    LastUpdateUserId = listA[index].Id


                };

                _repositoryProperty.Insert(entityTypeProperty);

            }


        }

        public int GetId()
        {
            var entity = _repository.GetAll();
            var list = new List<int>();

            foreach (var ent in entity)
            {
                list.Add(ent.Id);
                
            }

            var ran = new Random();
            int index = ran.Next(1, list.Count());

            return list[index];
        }


    }
}
