using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HydraTestProject.Authorization.Roles;
using HydraTestProject.Authorization.Users;
using HydraTestProject.Models.Core;
using HydraTestProject.Models.Tabels;
using HydraTestProject.MultiTenancy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json;

namespace HydraTestProject.EntityFrameworkCore
{
    public class HydraTestProjectDbContext : AbpZeroDbContext<Tenant, Role, User, HydraTestProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */


        //private readonly IHostingEnvironment environment; 

        public HydraTestProjectDbContext(DbContextOptions<HydraTestProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<CoreEntity> CoreEntities { get; set; }
        public DbSet<CoreEntityType> CoreEntityTypes { get; set; }
        public DbSet<CoreEntityTypeProperty> CoreEntityTypeProperties { get; set; }
        public DbSet<CoreEntityPropertyValue> CoreEntityPropertyValues { get; set; }

        public DbSet<TableA> TableAs { get; set; }
        public DbSet<TableB> TableBs { get; set; }
        public DbSet<TableC> TableCs { get; set; }
        public DbSet<TableD> TableDs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<CoreEntity>()
            //    .HasIndex(b => b.EntityTypeId)
            //    .ForSqlServerIsClustered(); 

            //modelBuilder.Entity<CoreEntityType>()
            //    .HasIndex(b => b.Name)
            //    .IsUnique();

            //modelBuilder.Entity<CoreEntityType>()
            //    .HasIndex(b => b.Path)
            //    .IsUnique();

            modelBuilder.Entity<CoreEntityTypeProperty>()
                .HasIndex(b => new {b.Name, b.EntityTypeId});
                

            //modelBuilder.Entity<CoreEntityTypeProperty>()
            //    .HasIndex(b => b.EntityTypeId)
            //    .ForSqlServerIsClustered();

            modelBuilder.Entity<CoreEntityPropertyValue>()
                .HasIndex(b => new {b.EntityId, b.EntityTypePropertyId})
                .IsUnique();

            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<User>().Ignore(a => a.DeleterUser);

            var jsonStringA = File.ReadAllText("name.json");
            var listA = JsonConvert.DeserializeObject<List<TableA>>(jsonStringA);
            modelBuilder.Entity<TableA>().HasData(listA);

            var jsonStringB = File.ReadAllText("name.json");
            var listB = JsonConvert.DeserializeObject<List<TableA>>(jsonStringB);
            modelBuilder.Entity<TableB>().HasData(listB);

            var jsonStringC = File.ReadAllText("name.json");
            var listC = JsonConvert.DeserializeObject<List<TableC>>(jsonStringB);
            modelBuilder.Entity<TableC>().HasData(listC);

            var jsonStringD = File.ReadAllText("name.json");
            var listD = JsonConvert.DeserializeObject<List<TableD>>(jsonStringD);
            modelBuilder.Entity<TableD>().HasData(listD);


        }
    }
}
