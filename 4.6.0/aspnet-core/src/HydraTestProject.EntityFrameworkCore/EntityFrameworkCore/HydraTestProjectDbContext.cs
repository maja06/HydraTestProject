using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HydraTestProject.Authorization.Roles;
using HydraTestProject.Authorization.Users;
using HydraTestProject.Models.Core;
using HydraTestProject.MultiTenancy;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace HydraTestProject.EntityFrameworkCore
{
    public class HydraTestProjectDbContext : AbpZeroDbContext<Tenant, Role, User, HydraTestProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public HydraTestProjectDbContext(DbContextOptions<HydraTestProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<CoreEntity> CoreEntities { get; set; }
        public DbSet<CoreEntityType> CoreEntityTypes { get; set; }
        public DbSet<CoreEntityTypeProperty> CoreEntityTypeProperties { get; set; }
        public DbSet<CoreEntityPropertyValue> CoreEntityPropertyValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoreEntity>()
                .HasIndex(b => b.EntityTypeId)
                .ForSqlServerIsClustered(); 

            //modelBuilder.Entity<CoreEntityType>()
            //    .HasIndex(b => b.Name)
            //    .IsUnique();

            //modelBuilder.Entity<CoreEntityType>()
            //    .HasIndex(b => b.Path)
            //    .IsUnique();

            modelBuilder.Entity<CoreEntityTypeProperty>()
                .HasIndex(b => new {b.Name, b.EntityTypeId});
                

            modelBuilder.Entity<CoreEntityTypeProperty>()
                .HasIndex(b => b.EntityTypeId)
                .ForSqlServerIsClustered();

            modelBuilder.Entity<CoreEntityPropertyValue>()
                .HasIndex(b => new {b.EntityId, b.EntityTypePropertyId})
                .IsUnique();

            base.OnModelCreating(modelBuilder);
            var jsonString = File.ReadAllText("int.json");
            var list = JsonConvert.DeserializeObject<List<MajaMegiBaneLLLLLLLLLLLL>>(jsonString);

            //var jsonString2 = File.ReadAllText("todo-items.json");
           // var list2 = JsonConvert.DeserializeObject<List<TodoItem>>(jsonSring2);
            modelBuilder.Entity<CoreEntityTypeProperty>().HasData(list);
            
        }
    }
}
