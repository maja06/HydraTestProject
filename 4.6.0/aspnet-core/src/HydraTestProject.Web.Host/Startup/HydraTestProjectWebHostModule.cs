using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HydraTestProject.Configuration;

namespace HydraTestProject.Web.Host.Startup
{
    [DependsOn(
       typeof(HydraTestProjectWebCoreModule))]
    public class HydraTestProjectWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public HydraTestProjectWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HydraTestProjectWebHostModule).GetAssembly());
        }
    }
}
