using System.Reflection;
using Abp.AutoMapper;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HydraTestProject.Authorization;
using HydraTestProject.DTO;
using HydraTestProject.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace HydraTestProject
{
    [DependsOn(
        typeof(HydraTestProjectCoreModule),
        typeof(AbpAutoMapperModule))]
    public class HydraTestProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<HydraTestProjectAuthorizationProvider>();


        }

        public override void Initialize()
        {
            var thisAssembly = typeof(HydraTestProjectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg =>
                {
                    cfg.AddProfiles(thisAssembly);

                    ////cfg.CreateMap<PropertyDto, CoreEntityTypeProperty>()
                    ////    .ForMember(dest => dest.Name, source => source.MapFrom(x => x.PropName))
                    ////    .ForMember(dest => dest.DbType, source => source.MapFrom(y => y.PropType));
                });

        }
    }
}
