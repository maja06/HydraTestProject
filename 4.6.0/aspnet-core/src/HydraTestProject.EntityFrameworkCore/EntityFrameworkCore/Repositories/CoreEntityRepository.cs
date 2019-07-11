using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.UI;
using Castle.Windsor.Installer;
using HydraTestProject.Models.Core;
using HydraTestProject.Models.Tabels;
using HydraTestProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HydraTestProject.EntityFrameworkCore.Repositories
{
    public class CoreEntityRepository : HydraTestProjectRepositoryBase<CoreEntity, Guid>, ICoreEntityRepository
    {
        public CoreEntityRepository(IDbContextProvider<HydraTestProjectDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        //public async Task<object> Lambda()
        //{
        //    var entities = Context.CoreEntities;
        //    var entityTypes = Context.CoreEntityTypes;
        //    var entityValues = Context.CoreEntityPropertyValues;
        //    var entityProperties = Context.CoreEntityTypeProperties;

        //    var query = entities
        //        .Join(entityTypes, e => e.EntityTypeId, t => t.Id, (e, t) => new {e, t})
        //        .Join(entityValues, m => m.e.Id, v => v.EntityId, (m, v) => new {m, v})
        //        .Join(entityProperties, n => n.v.EntityTypePropertyId, p => p.Id, (n, p) => new {n, p})
        //        .GroupJoin(entities, all => new {all.n.v.EntityId, all.p.Name }, lorryEntity => lorryEntity.Id,
        //            (all, lorryOuter) => new {PropName = lorryOuter, Value = all.n.v.EntityId});
        //        //    .Join(entityTypes, e => e.EntityTypeId, t => t.Id, (e, t) => new {e, t})
        //        //.Join(entityValues, m => m.e.Id, v => v.EntityId, (m, v) => new {m, v})
        //        //.Join(entityProperties, n => n.v.EntityTypePropertyId, p => p.Id, (n, p) => new {n, p})
        //        //.GroupJoin(entityValues, kurtEntity => kurtEntity.n.m.e.Id, values => values.GuidValue,(kurtEntity, kurtOuter) => new {Values = kurtOuter, Name = "Kurt"}).FirstOrDefault();

   
 

        public async Task<object> TestLambda()
        {
            // Sledeće tri linije su uvijek iste, s tim što u ovom primjeru koristimo zakucani tip JoinResult, a u realnom primjeru taj tip će biti dinamički generisan
             var entities = Context.CoreEntities.Include(x => x.EntityType);
            var joinResult = entities
                .Join(Context.CoreEntityPropertyValues,
                    e => e.Id, v => v.EntityId,
                    (e, v) => new JoinResult { __Entity = e, __Value = v });
            joinResult = joinResult
                .Join(Context.CoreEntityTypeProperties,
                    ev => ev.__Value.EntityTypePropertyId,
                    p => p.Id,
                    (ev, p) => new JoinResult { __Entity = ev.__Entity, __Property = p, __Value = ev.__Value });

            //////////////////// Filter za staticke propertije
            joinResult = joinResult.Where(
                x => x.__Entity.Name == "Sallee" && x.__Entity.EntityType.Name == "Dorey");



            ///////////////// Odavde kreće generisanje join komandi
            joinResult = joinResult.GroupJoin(
              Context.CoreEntities,
              evp => new { Key = (Guid)evp.__Value.GuidValue, PropName = evp.__Property.Name },
              ex => new { Key = ex.Id, PropName = "Lorry" },
              // Ođe prenosimo prethodne vrijednosti svih propertija, osim onog propertija koga se tiče ovaj join.
              // U ovom slučaju to je Lorry
              (evp, ex) => new
              {
                  __Entity = evp.__Entity,
                  __Property = evp.__Property,
                  __Value = evp.__Value,
                  Kurt = evp.Kurt,
                  Pearla = evp.Pearla,
                  Kessia = evp.Kessia,
                  Lorry = ex
              })
          .SelectMany(
              ex => ex.Lorry.DefaultIfEmpty(),
              // Ođe prenosimo prethodne vrijednosti svih propertija, osim onog propertija koga se tiče ovaj join.
              // U ovom slučaju to je Lorry
              (evp, ex) => new JoinResult
              {
                  __Entity = evp.__Entity,
                  __Property = evp.__Property,
                  __Value = evp.__Value,
                  Kurt = evp.Kurt,
                  Pearla = evp.Pearla,
                  Kessia = evp.Kessia,
                  Lorry = ex
              });

            joinResult = joinResult.GroupJoin(
                    Context.CoreEntities,
                    evp => new { Key = (Guid)evp.__Value.GuidValue, PropName = evp.__Property.Name },
                    ex => new { Key = ex.Id, PropName = "Kurt" },

                    // Ođe prenosimo prethodne vrijednosti svih propertija, osim onog propertija koga se tiče ovaj join.
                    // U ovom slučaju to je Kurt
                    (evp, ex) => new
                    {
                        __Entity = evp.__Entity,
                        __Property = evp.__Property,
                        __Value = evp.__Value,
                        Pearla = evp.Pearla,
                        Kessia = evp.Kessia,
                        Lorry = evp.Lorry,
                        Kurt = ex,
                    })
                .SelectMany(
                    ex => ex.Kurt.DefaultIfEmpty(),

                    // Ođe prenosimo prethodne vrijednosti svih propertija, osim onog propertija koga se tiče ovaj join.
                    // U ovom slučaju to je Kurt
                    (evp, ex) => new JoinResult
                    {
                        __Entity = evp.__Entity,
                        __Property = evp.__Property,
                        __Value = evp.__Value,
                        Pearla = evp.Pearla,
                        Kessia = evp.Kessia,
                        Lorry = evp.Lorry,
                        Kurt = ex,
                    });

            joinResult = joinResult.GroupJoin(
                    Context.CoreEntities,
                    evp => new { Key = (Guid)evp.__Value.GuidValue, PropName = evp.__Property.Name },
                    ex => new { Key = ex.Id, PropName = "Pearla" },

                    // Ođe prenosimo prethodne vrijednosti svih propertija, osim onog propertija koga se tiče ovaj join.
                    // U ovom slučaju to je Pearla
                    (evp, ex) => new
                    {
                        __Entity = evp.__Entity,
                        __Property = evp.__Property,
                        __Value = evp.__Value,
                        Pearla = ex,
                        Kessia = evp.Kessia,
                        Lorry = evp.Lorry,
                        Kurt = evp.Kurt,
                    })
                .SelectMany(
                    ex => ex.Pearla.DefaultIfEmpty(),

                    // Ođe prenosimo prethodne vrijednosti svih propertija, osim onog propertija koga se tiče ovaj join.
                    // U ovom slučaju to je Pearla
                    (evp, ex) => new JoinResult
                    {
                        __Entity = evp.__Entity,
                        __Property = evp.__Property,
                        __Value = evp.__Value,
                        Pearla = ex,
                        Kessia = evp.Kessia,
                        Lorry = evp.Lorry,
                        Kurt = evp.Kurt,
                    });

            joinResult = joinResult.GroupJoin(
                    Context.TableAs,
                    evp => new { Key = (int)evp.__Value.IntValue, PropName = evp.__Property.Name },
                    ex => new { Key = ex.Id, PropName = "Kessia" },

                    // Ođe prenosimo prethodne vrijednosti svih propertija, osim onog propertija koga se tiče ovaj join.
                    // U ovom slučaju to je Kessia
                    (evp, ex) => new
                    {
                        __Entity = evp.__Entity,
                        __Property = evp.__Property,
                        __Value = evp.__Value,
                        Pearla = evp.Pearla,
                        Kessia = ex,
                        Lorry = evp.Lorry,
                        Kurt = evp.Kurt,
                    })
                .SelectMany(
                    ex => ex.Kessia.DefaultIfEmpty(),

                    // Ođe prenosimo prethodne vrijednosti svih propertija, osim onog propertija koga se tiče ovaj join.
                    // U ovom slučaju to je Kessia
                    (evp, ex) => new JoinResult
                    {
                        __Entity = evp.__Entity,
                        __Property = evp.__Property,
                        __Value = evp.__Value,
                        Pearla = evp.Pearla,
                        Kessia = ex,
                        Lorry = evp.Lorry,
                        Kurt = evp.Kurt,
                    });

            // Priprema za finalni rezultat
            var intermediateResult = joinResult.Select(x =>
                new
                {
                    Id = x.__Entity.Id,
                    Name = x.__Entity.Name,
                    Lorry = x.Lorry.Name,
                    Kurt = x.Kurt.Name,
                    Pearla = x.Pearla.Name,
                    Kessia = x.Kessia.Name
                });

            // I na kraju, transformacija IntermediateResult u FinalResult
            var result = intermediateResult.GroupBy(x => x.Id)
                .Select(grouped => new
                {
                    Id = grouped.Key,
                    Name = grouped.Max(x => x.Name),
                    Lorry = grouped.Max(x => x.Lorry),
                    Kurt = grouped.Max(x => x.Kurt),
                    Pearla = grouped.Max(x => x.Pearla),
                    Kessia = grouped.Max(x => x.Kessia)
                });

            // Filtriranje dinamickih propertija
            result = result.Where(x => x.Kessia.Contains("a"));

            var list = result.ToList();
            return list;
        }


        public async Task<object> Test()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            var query = from e in Context.CoreEntities
                join t in Context.CoreEntityTypes on e.EntityTypeId equals t.Id
                join v in Context.CoreEntityPropertyValues on e.Id equals v.EntityId
                join p in Context.CoreEntityTypeProperties on v.EntityTypePropertyId equals p.Id
                join lorryEntity in Context.CoreEntities on new {Id = (Guid) v.GuidValue, PropName = p.Name} equals new
                    {Id = lorryEntity.Id, PropName = "Lorry"} into lorryOuter
                from lorryEntity in lorryOuter.DefaultIfEmpty()
                join kurtEntity in Context.CoreEntities on new {Id = (Guid) v.GuidValue, PropName = p.Name} equals new
                    {Id = kurtEntity.Id, PropName = "Kurt"} into kurtOuter
                from kurtEntity in kurtOuter.DefaultIfEmpty()
                join pearlaEntity in Context.CoreEntities on new {Id = (Guid) v.GuidValue, PropName = p.Name} equals new
                    {Id = pearlaEntity.Id, PropName = "Pearla"} into pearlaOuter
                from pearlaEntity in pearlaOuter.DefaultIfEmpty()
                join tableAEntity in Context.TableAs on new {Id = (int) v.IntValue, PropName = p.Name} equals new
                    {Id = tableAEntity.Id, PropName = "Kessia"} into tableAOuter
                from tableAEntity in tableAOuter.DefaultIfEmpty()
                where e.Name == "Sallee" && t.Name == "Dorey" &&
                      (p.ReferenceType == CoreEntityTypeProperty.ReferenceTypeEnum.InternalReference &&
                       v.GuidValue != null ||
                       p.ReferenceType == CoreEntityTypeProperty.ReferenceTypeEnum.ExternalReference)
                select new
                {
                    Id = e.Id,
                    Name = e.Name,
                    Lorrey = lorryEntity.Name,
                    Kurt = kurtEntity.Name,
                    Pearla = pearlaEntity.Name,
                    Kessia = tableAEntity.Name
                }
                into ungrouped
                group ungrouped by ungrouped.Id
                into grouped
                select new
                {
                    Id = grouped.Key,
                    Name = grouped.Max(x => x.Name),
                    Lorrey = grouped.Max(x => x.Lorrey),
                    Kurt = grouped.Max(x => x.Kurt),
                    Pearla = grouped.Max(x => x.Pearla),
                    Kessia = grouped.Max(x => x.Kessia),
                };

            query = query.Where(x => x.Kessia.Contains("a"));
            try
            {
                
                var result = await query.ToListAsync();
                s.Stop();
                return new {result,s.Elapsed};
                //return result;

            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.ToString());
            }
        }
    }

    internal class JoinResult
    {
        public CoreEntity __Entity { get; set; }
        public CoreEntityPropertyValue __Value { get; set; }
        public CoreEntityTypeProperty __Property { get; set; }
        public CoreEntity Lorry { get; set; }
        public TableA Kessia { get; set; }
        public CoreEntity Kurt { get; set; }
        public CoreEntity Pearla { get; set; }
    }
}
