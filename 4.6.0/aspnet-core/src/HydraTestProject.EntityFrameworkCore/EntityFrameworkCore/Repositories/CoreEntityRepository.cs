using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.UI;
using HydraTestProject.Models.Core;
using HydraTestProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HydraTestProject.EntityFrameworkCore.Repositories
{
    public class CoreEntityRepository : HydraTestProjectRepositoryBase<CoreEntity, Guid>, ICoreEntityRepository
    {
        public CoreEntityRepository(IDbContextProvider<HydraTestProjectDbContext> dbContextProvider) : base(dbContextProvider)
        {
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
}
