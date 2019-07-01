using System.Reflection;
using Abp.AutoMapper;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HydraTestProject.Authorization;
using Microsoft.AspNetCore.Http;

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
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
