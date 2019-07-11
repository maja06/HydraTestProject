using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using HydraTestProject.Models;
using HydraTestProject.Models.Core;
using HydraTestProject.Models.Tabels;
using HydraTestProject.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HydraTestProject.Services
{
    public class SeedService : HydraTestProjectAppServiceBase, ISeedService
    {
        private readonly IRepository<CoreEntityType> _repositoryType;
        private readonly IRepository<CoreEntityTypeProperty> _repositoryProperty;
        private readonly ICoreEntityRepository _repositoryEntity;
        private readonly IRepository<CoreEntityPropertyValue> _repositoryValue;

        private readonly List<Guid> guids = new List<Guid>();

        public SeedService(IRepository<CoreEntityType> repositoryType,
            IRepository<CoreEntityTypeProperty> repositoryProperty, ICoreEntityRepository repositoryEntity,
            IRepository<CoreEntityPropertyValue> repositoryValue)
        {
            _repositoryType = repositoryType;
            _repositoryProperty = repositoryProperty;
            _repositoryEntity = repositoryEntity;
            _repositoryValue = repositoryValue;
        }

        public void CreateTypeNew(int n)
        {
            var jsonString = File.ReadAllText("MajaMegi.json");
            var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);

            for (var i = 0; i < n; i++)
            {
                var list = new List<CoreEntityTypeProperty>();
                Random r = new Random();
                int index = r.Next(1, 1000);
                int index1 = r.Next(1, 1000);

                var entityType = new CoreEntityType()
                {
                    Name = $"Type{i}",
                    Path = listA[index].Path,
                    Description = listA[index].Description,
                    IconName = listA[index1].Name,
                    InsertIpAddress = listA[index].IpAddress,
                    InsertTime = listA[index].Date,
                    InsertUserId = listA[index].Id,
                    IsCustom = listA[index].Bool,
                    IsActive = listA[index].Bool,
                    LastUpdateIpAddress = listA[index1].IpAddress,
                    LastUpdateTime = listA[index1].Date,
                    ParentEntityTypeId = null,
                    LastUpdateUserId = listA[index1].Id,
                    Level = listA[index1].Id,
                    Order = listA[index].Id,

                };

                _repositoryType.Insert(entityType);
            }
        }

        public async Task<TimeSpan> CreateEntityNew(int n)
        {
            using (var uow = UnitOfWorkManager.Begin(new UnitOfWorkOptions
            {
                Scope = TransactionScopeOption.RequiresNew
            }))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var jsonString = await File.ReadAllTextAsync("MajaMegi.json");
                var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);

                var types = await _repositoryType.GetAll().Include(x => x.EntityTypeProperties).ToListAsync();
                foreach (var type in types)
                {
                    for (var i = 0; i < n / types.Count(); i++)
                    {
                        Random r = new Random();
                        int index = r.Next(1, 1000);
                        int index1 = r.Next(1, 1000);

                        var entity = new CoreEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = listA[index].Name,
                            InsertUserId = listA[index].Id,
                            InsertIpAddress = listA[index].IpAddress,
                            Description = listA[index].Description,
                            InsertTime = listA[index].Date,
                            LastUpdateIpAddress = listA[index1].IpAddress,
                            LastUpdateTime = listA[index1].Date,
                            LastUpdateUserId = listA[index].Id,
                            EntityType = type
                        };

                        guids.Add(entity.Id);

                        foreach (var prop in type.EntityTypeProperties)
                        {
                            Random valueRandom = new Random();
                            int valueIndex = valueRandom.Next(1, 1000);
                            int valueIndex1 = valueRandom.Next(1, 1000);
                            var value = new CoreEntityPropertyValue
                            {
                                DataTimeValue = listA[valueIndex].Date,
                                DecimalValue = 1.111m,
                                Entity = entity,
                                EntityTypeProperty = prop,
                                InsertUserId = listA[valueIndex].Id,
                                InsertIpAddress = listA[valueIndex].IpAddress,
                                InsertTime = listA[valueIndex].Date,
                                LastUpdateIpAddress = listA[valueIndex].IpAddress,
                                LastUpdateTime = listA[valueIndex].Date,
                                LastUpdateUserId = listA[valueIndex].Id,
                                TextValue = listA[valueIndex1].Name,
                            };

                            switch (prop.ReferenceType)
                            {
                                case CoreEntityTypeProperty.ReferenceTypeEnum.NoReference:
                                    break;

                                case CoreEntityTypeProperty.ReferenceTypeEnum.InternalReference:
                                    var guid = GetRandomEntityGuid();
                                    if (guid == Guid.Empty)
                                    {
                                        break;
                                    }

                                    while (guid == entity.Id)
                                    {
                                        guid = GetRandomEntityGuid();
                                    }

                                    value.GuidValue = guid;
                                    break;

                                case CoreEntityTypeProperty.ReferenceTypeEnum.ExternalReference:
                                    value.IntValue = valueRandom.Next(1, 10);
                                    break;
                            }

                            entity.EntityPropertyValues.Add(value);
                        }

                        await _repositoryEntity.InsertAsync(entity);
                    }
                }

                await uow.CompleteAsync();
                stopwatch.Stop();
                return stopwatch.Elapsed;
            }
        }

        public void CreatePropertiesNew()
        {
            var jsonString = File.ReadAllText("MajaMegi.json");
            var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);

            var types = _repositoryType.GetAll().ToList();

            foreach (var type in types)
            {
                for (var j = 0; j < 3; j++)
                {
                    Random ran = new Random();
                    int index2 = ran.Next(1, 1000);
                    int index3 = ran.Next(1, 1000);

                    var entityTypeProperty = new CoreEntityTypeProperty()
                    {
                        Name = $"{type.Name}Property{j}",
                        DbPrecision = listA[index3].Name,
                        DefaultValue = listA[index2].Path,
                        Description = listA[index3].Description,
                        DbType = "string",
                        IsProtected = listA[index2].Bool,
                        IsRequired = listA[index3].Bool,
                        EntityTypeId = type.Id,
                        InsertIpAddress = listA[index2].IpAddress,
                        InsertTime = listA[index2].Date,
                        InsertUseId = listA[index2].Id,
                        IsCustom = listA[index3].Bool,
                        LastUpdateIpAddress = listA[index3].IpAddress,
                        LastUpdateTime = listA[index3].Date,
                        LastUpdateUserId = listA[index3].Id,
                        ReferenceTableId = null,
                        PropertyOrder = listA[index2].Id,
                        IsTranslatableValue = listA[index2].Bool,
                        ReferenceType = CoreEntityTypeProperty.ReferenceTypeEnum.NoReference

                    };

                    type.EntityTypeProperties.Add(entityTypeProperty);
                    _repositoryProperty.Insert(entityTypeProperty);

                }

                for (var k = 0; k < 3; k++)
                {
                    Random ran = new Random();
                    int index4 = ran.Next(1, 1000);
                    int index5 = ran.Next(1, 1000);

                    var entityTypeProperty = new CoreEntityTypeProperty()
                    {
                        Name = $"{type.Name}Property{k+3}",
                        DbPrecision = listA[index5].Name,
                        DefaultValue = listA[index4].Path,
                        Description = listA[index5].Description,
                        DbType = "Guid",
                        IsProtected = listA[index4].Bool,
                        IsRequired = listA[index5].Bool,
                        EntityTypeId = type.Id,
                        InsertIpAddress = listA[index4].IpAddress,
                        InsertTime = listA[index4].Date,
                        InsertUseId = listA[index4].Id,
                        IsCustom = listA[index4].Bool,
                        LastUpdateIpAddress = listA[index5].IpAddress,
                        LastUpdateTime = listA[index5].Date,
                        LastUpdateUserId = listA[index5].Id,
                        ReferenceTableId = null,
                        PropertyOrder = listA[index4].Id,
                        IsTranslatableValue = listA[index5].Bool,
                        ReferenceType = CoreEntityTypeProperty.ReferenceTypeEnum.InternalReference,

                    };

                    type.EntityTypeProperties.Add(entityTypeProperty);
                    _repositoryProperty.Insert(entityTypeProperty);
                }

                for (var m = 0; m < 4; m++)
                {
                    Random ran = new Random();
                    int index6 = ran.Next(1, 1000);
                    int index7 = ran.Next(1, 1000);

                    var entityTypeProperty = new CoreEntityTypeProperty()
                    {
                        Name = $"{type.Name}Property{m+6}",
                        DbPrecision = listA[index7].Name,
                        DefaultValue = listA[index6].Path,
                        Description = listA[index7].Description,
                        DbType = "int",
                        IsProtected = listA[index6].Bool,
                        IsRequired = listA[index7].Bool,
                        EntityTypeId = type.Id,
                        InsertIpAddress = listA[index6].IpAddress,
                        InsertTime = listA[index6].Date,
                        InsertUseId = listA[index6].Id,
                        IsCustom = listA[index6].Bool,
                        LastUpdateIpAddress = listA[index7].IpAddress,
                        LastUpdateTime = listA[index7].Date,
                        LastUpdateUserId = listA[index7].Id,
                        ReferenceTableId = ran.Next(1, 4),
                        PropertyOrder = listA[index6].Id,
                        IsTranslatableValue = listA[index7].Bool,
                        ReferenceType = CoreEntityTypeProperty.ReferenceTypeEnum.ExternalReference,

                    };

                    type.EntityTypeProperties.Add(entityTypeProperty);
                    _repositoryProperty.Insert(entityTypeProperty);

                }
            }

        }
        //Konacna metoda za kreiranje tipova sa random podatke
        public void CreateEntityType(int n)
        {
            var jsonString = File.ReadAllText("MajaMegi.json");
            var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);

            for (var i = 0; i < n; i++)
            {
                var list = new List<CoreEntityTypeProperty>();
                Random r = new Random();
                int index = r.Next(1, 1000);
                int index1 = r.Next(1, 1000);

                var entityType = new CoreEntityType()
                {
                    Name = listA[index].Name,
                    Path = listA[index].Path,
                    Description = listA[index].Description,
                    IconName = listA[index1].Name,
                    InsertIpAddress = listA[index].IpAddress,
                    InsertTime = listA[index].Date,
                    InsertUserId = listA[index].Id,
                    IsCustom = listA[index].Bool,
                    IsActive = listA[index].Bool,
                    LastUpdateIpAddress = listA[index1].IpAddress,
                    LastUpdateTime = listA[index1].Date,
                    ParentEntityTypeId = null,
                    LastUpdateUserId = listA[index1].Id,
                    Level = listA[index1].Id,
                    Order = listA[index].Id,

                };

                _repositoryType.Insert(entityType);

            }
        }

        //Konacna metoda za kreiranje entiteta sa vrijednostima i propertijima koristeci random podatke
        public async Task<TimeSpan> CreateEntity(int n)
        {
            using (var uow = UnitOfWorkManager.Begin(new UnitOfWorkOptions
            {
                Scope = TransactionScopeOption.RequiresNew
            }))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var jsonString = await File.ReadAllTextAsync("MajaMegi.json");
                var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);

                var types = await _repositoryType.GetAll().Include(x => x.EntityTypeProperties).ToListAsync();
                foreach (var type in types)
                {
                    for (var i = 0; i < n / types.Count(); i++)
                    {
                        Random r = new Random();
                        int index = r.Next(1, 1000);
                        int index1 = r.Next(1, 1000);

                        var entity = new CoreEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = listA[index].Name,
                            InsertUserId = listA[index].Id,
                            InsertIpAddress = listA[index].IpAddress,
                            Description = listA[index].Description,
                            InsertTime = listA[index].Date,
                            LastUpdateIpAddress = listA[index1].IpAddress,
                            LastUpdateTime = listA[index1].Date,
                            LastUpdateUserId = listA[index].Id,
                            EntityType = type
                        };

                        guids.Add(entity.Id);

                        foreach (var prop in type.EntityTypeProperties)
                        {
                            Random valueRandom = new Random();
                            int valueIndex = valueRandom.Next(1, 1000);
                            int valueIndex1 = valueRandom.Next(1, 1000);
                            var value = new CoreEntityPropertyValue
                            {
                                DataTimeValue = listA[valueIndex].Date,
                                DecimalValue = 1.111m,
                                Entity = entity,
                                EntityTypeProperty = prop,
                                InsertUserId = listA[valueIndex].Id,
                                InsertIpAddress = listA[valueIndex].IpAddress,
                                InsertTime = listA[valueIndex].Date,
                                LastUpdateIpAddress = listA[valueIndex].IpAddress,
                                LastUpdateTime = listA[valueIndex].Date,
                                LastUpdateUserId = listA[valueIndex].Id,
                                TextValue = listA[valueIndex1].Name,
                            };

                            switch (prop.ReferenceType)
                            {
                                case CoreEntityTypeProperty.ReferenceTypeEnum.NoReference:
                                    break;

                                case CoreEntityTypeProperty.ReferenceTypeEnum.InternalReference:
                                    var guid = GetRandomEntityGuid();
                                    if (guid == Guid.Empty)
                                    {
                                        break;
                                    }

                                    while (guid == entity.Id)
                                    {
                                        guid = GetRandomEntityGuid();
                                    }

                                    value.GuidValue = guid;
                                    break;

                                case CoreEntityTypeProperty.ReferenceTypeEnum.ExternalReference:
                                    value.IntValue = valueRandom.Next(1, 10);
                                    break;
                            }

                            entity.EntityPropertyValues.Add(value);
                        }

                        await _repositoryEntity.InsertAsync(entity);
                    }
                }

                await uow.CompleteAsync();
                stopwatch.Stop();
                return stopwatch.Elapsed;
            }
        }

        //Probna metoda za kreiranje vrijednosti sa random podacima
        public async Task CreateValue()
        {
            var jsonString = File.ReadAllText("MajaMegi.json");
            var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);

            

            ////var entityTypes = _repositoryType.GetAll().Include(x => x.EntityTypeProperties);

            var entities = _repositoryEntity.GetAll().Include(x => x.EntityType)
                .ThenInclude(x => x.EntityTypeProperties);
            foreach (var entity in entities)
            {

                foreach (var prop in entity.EntityType.EntityTypeProperties)
                {
                    Random r = new Random();
                    int index = r.Next(1, 1000);
                    int index1 = r.Next(1, 1000);
                    var value = new CoreEntityPropertyValue
                    {
                        DataTimeValue = listA[index].Date,
                        DecimalValue = 1.111m,
                        EntityId = entity.Id,
                        EntityTypePropertyId = prop.Id,
                        InsertUserId = listA[index].Id,
                        InsertIpAddress = listA[index].IpAddress,
                        InsertTime = listA[index].Date,
                        LastUpdateIpAddress = listA[index].IpAddress,
                        LastUpdateTime = listA[index].Date,
                        LastUpdateUserId = listA[index].Id,
                        TextValue = listA[index1].Name,
                    };

                    switch (prop.ReferenceType)
                    {
                        case CoreEntityTypeProperty.ReferenceTypeEnum.NoReference:
                            break;

                        case CoreEntityTypeProperty.ReferenceTypeEnum.InternalReference:
                            var guid = GetRandomEntityGuid();
                            while (guid == entity.Id)
                            {
                                guid = GetRandomEntityGuid();
                            }

                            value.GuidValue = guid;
                            break;

                        case CoreEntityTypeProperty.ReferenceTypeEnum.ExternalReference:
                            value.IntValue = r.Next(1, 10);
                            break;
                    }

                    _repositoryValue.Insert(value);
                }
            }

            ////foreach (var entity in entityTypes)
            ////{
            ////    var properties = entity.EntityTypeProperties;

            ////    foreach (var property in properties)
            ////    {
            ////        switch (property.ReferenceType)
            ////        {
            ////            case CoreEntityTypeProperty.ReferenceTypeEnum.NoReference:
            ////                {
            ////                    var value = new CoreEntityPropertyValue()
            ////                    {
            ////                        DataTimeValue = listA[index].Date,
            ////                        DecimalValue = 1.111m,
            ////                        EntityId = GetRandomEntityGuid(),
            ////                        EntityTypePropertyId = GetRandomEntityPropertyId(),
            ////                        InsertUserId = listA[index].Id,
            ////                        InsertIpAddress = listA[index].IpAddress,
            ////                        InsertTime = listA[index].Date,
            ////                        LastUpdateIpAddress = listA[index].IpAddress,
            ////                        LastUpdateTime = listA[index].Date,
            ////                        LastUpdateUserId = listA[index].Id,
            ////                        TextValue = listA[index1].Name,
            ////                        GuidValue = null,
            ////                        IntValue = null
            ////                    };

            ////                    while (PostojiLi(value.EntityId,value.EntityTypePropertyId))
            ////                    {
            ////                        value.EntityId = GetRandomEntityGuid();
            ////                        value.EntityTypePropertyId = GetRandomEntityPropertyId();
            ////                    }
            ////                    _repositoryValue.Insert(value);
            ////                    break;
            ////                }
            ////            case CoreEntityTypeProperty.ReferenceTypeEnum.InternalReference:
            ////                {
            ////                    var value = new CoreEntityPropertyValue()
            ////                    {
            ////                        DataTimeValue = listA[index].Date,
            ////                        DecimalValue = 1.111m,
            ////                        EntityId = GetRandomEntityGuid(),
            ////                        EntityTypePropertyId = GetRandomEntityPropertyId(),
            ////                        InsertUserId = listA[index].Id,
            ////                        InsertIpAddress = listA[index].IpAddress,
            ////                        InsertTime = listA[index].Date,
            ////                        LastUpdateIpAddress = listA[index].IpAddress,
            ////                        LastUpdateTime = listA[index].Date,
            ////                        LastUpdateUserId = listA[index].Id,
            ////                        TextValue = null,
            ////                        IntValue = null
            ////                    };
            ////                    while (PostojiLi(value.EntityId, value.EntityTypePropertyId))
            ////                    {
            ////                        value.EntityId = GetRandomEntityGuid();
            ////                        value.EntityTypePropertyId = GetRandomEntityPropertyId();
            ////                    }
            ////                    value.GuidValue = GetGuid(value.EntityId);
            ////                    _repositoryValue.Insert(value);
            ////                    break;
            ////                }
            ////            default:
            ////                {
            ////                    var value = new CoreEntityPropertyValue()
            ////                    {
            ////                        DataTimeValue = listA[index].Date,
            ////                        DecimalValue = 1.111m,
            ////                        EntityId = GetRandomEntityGuid(),
            ////                        EntityTypePropertyId = GetRandomEntityPropertyId(),
            ////                        InsertUserId = listA[index].Id,
            ////                        InsertIpAddress = listA[index].IpAddress,
            ////                        InsertTime = listA[index].Date,
            ////                        LastUpdateIpAddress = listA[index].IpAddress,
            ////                        LastUpdateTime = listA[index].Date,
            ////                        LastUpdateUserId = listA[index].Id,
            ////                        TextValue = null,
            ////                        GuidValue = null,
            ////                        IntValue = r.Next(1, 10)

            ////                    };
            ////                    while (PostojiLi(value.EntityId, value.EntityTypePropertyId))
            ////                    {
            ////                        value.EntityId = GetRandomEntityGuid();
            ////                        value.EntityTypePropertyId = GetRandomEntityPropertyId();
            ////                    }
            ////                    _repositoryValue.Insert(value);
            ////                    break;
            ////                }

            ////        }
            ////    }
            ////}
        }

        //Probna metoda za kreiranje propertija sa random podacima
        public void CreateProperties()
        {
            var jsonString = File.ReadAllText("MajaMegi.json");
            var listA = JsonConvert.DeserializeObject<List<TestClass>>(jsonString);

            var types = _repositoryType.GetAll().ToList();

            foreach (var type in types)
            {
                for (var j = 0; j < 3; j++)
                {
                    Random ran = new Random();
                    int index2 = ran.Next(1, 1000);
                    int index3 = ran.Next(1, 1000);

                    var entityTypeProperty = new CoreEntityTypeProperty()
                    {
                        Name = listA[index2].Name,
                        DbPrecision = listA[index3].Name,
                        DefaultValue = listA[index2].Path,
                        Description = listA[index3].Description,
                        DbType = "string",
                        IsProtected = listA[index2].Bool,
                        IsRequired = listA[index3].Bool,
                        EntityTypeId = type.Id,
                        InsertIpAddress = listA[index2].IpAddress,
                        InsertTime = listA[index2].Date,
                        InsertUseId = listA[index2].Id,
                        IsCustom = listA[index3].Bool,
                        LastUpdateIpAddress = listA[index3].IpAddress,
                        LastUpdateTime = listA[index3].Date,
                        LastUpdateUserId = listA[index3].Id,
                        ReferenceTableId = null,
                        PropertyOrder = listA[index2].Id,
                        IsTranslatableValue = listA[index2].Bool,
                        ReferenceType = CoreEntityTypeProperty.ReferenceTypeEnum.NoReference

                    };

                    type.EntityTypeProperties.Add(entityTypeProperty);
                    _repositoryProperty.Insert(entityTypeProperty);

                }

                for (var k = 0; k < 3; k++)
                {
                    Random ran = new Random();
                    int index4 = ran.Next(1, 1000);
                    int index5 = ran.Next(1, 1000);

                    var entityTypeProperty = new CoreEntityTypeProperty()
                    {
                        Name = listA[index4].Name,
                        DbPrecision = listA[index5].Name,
                        DefaultValue = listA[index4].Path,
                        Description = listA[index5].Description,
                        DbType = "Guid",
                        IsProtected = listA[index4].Bool,
                        IsRequired = listA[index5].Bool,
                        EntityTypeId = type.Id,
                        InsertIpAddress = listA[index4].IpAddress,
                        InsertTime = listA[index4].Date,
                        InsertUseId = listA[index4].Id,
                        IsCustom = listA[index4].Bool,
                        LastUpdateIpAddress = listA[index5].IpAddress,
                        LastUpdateTime = listA[index5].Date,
                        LastUpdateUserId = listA[index5].Id,
                        ReferenceTableId = null,
                        PropertyOrder = listA[index4].Id,
                        IsTranslatableValue = listA[index5].Bool,
                        ReferenceType = CoreEntityTypeProperty.ReferenceTypeEnum.InternalReference,

                    };

                    type.EntityTypeProperties.Add(entityTypeProperty);
                    _repositoryProperty.Insert(entityTypeProperty);
                }

                for (var m = 0; m < 4; m++)
                {
                    Random ran = new Random();
                    int index6 = ran.Next(1, 1000);
                    int index7 = ran.Next(1, 1000);

                    var entityTypeProperty = new CoreEntityTypeProperty()
                    {
                        Name = listA[index6].Name,
                        DbPrecision = listA[index7].Name,
                        DefaultValue = listA[index6].Path,
                        Description = listA[index7].Description,
                        DbType = "int",
                        IsProtected = listA[index6].Bool,
                        IsRequired = listA[index7].Bool,
                        EntityTypeId = type.Id,
                        InsertIpAddress = listA[index6].IpAddress,
                        InsertTime = listA[index6].Date,
                        InsertUseId = listA[index6].Id,
                        IsCustom = listA[index6].Bool,
                        LastUpdateIpAddress = listA[index7].IpAddress,
                        LastUpdateTime = listA[index7].Date,
                        LastUpdateUserId = listA[index7].Id,
                        ReferenceTableId = ran.Next(1, 4),
                        PropertyOrder = listA[index6].Id,
                        IsTranslatableValue = listA[index7].Bool,
                        ReferenceType = CoreEntityTypeProperty.ReferenceTypeEnum.ExternalReference,

                    };

                    type.EntityTypeProperties.Add(entityTypeProperty);
                    _repositoryProperty.Insert(entityTypeProperty);

                }
            }
        }

        //Glupkasta metoda
        public int GetRandomEntityTypeId()
        {
            var entity = _repositoryType.GetAll();
            var list = new List<int>();

            foreach (var ent in entity)
            {
                list.Add(ent.Id);
            }

            var ran = new Random();
            var index = ran.Next(1, list.Count());

            return list[index];
        }

        //Glupkasta metoda
        public int GetRandomEntityPropertyId()
        {
            var entity = _repositoryProperty.GetAll();
            var list = new List<int>();

            foreach (var ent in entity)
            {
                list.Add(ent.Id);
            }

            var ran = new Random();
            var index = ran.Next(1, list.Count());

            return list[index];
        }

        //Glupkasta metoda
        public async Task<object> Get()
        {
            return await _repositoryEntity.Test();
        }

        //Glupkasta metoda
        public object GetLambda()
        {
            return _repositoryEntity.TestLambda();
        }

        //Glupkasta metoda-- nije, nije, ona je pcelica--to se malo Megi m
        public Guid GetRandomEntityGuid()
        {
            var count = guids.Count;
            if (count < 2)
            {
                return Guid.Empty;
            }

            var ran = new Random();
            var index = ran.Next(0, count - 1);

            var guid = guids[index];
            return guid;
        }

        //Glupkasta metoda
        public Guid GetGuid(Guid input)
        {
            var entities = _repositoryEntity.GetAll().ToList();
            var entityId = entities.First(x => x.Id != input).Id;
            
            return entityId;
        }
    }
}
