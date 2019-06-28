using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using HydraTestProject.Configuration;
using HydraTestProject.Web;

namespace HydraTestProject.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class HydraTestProjectDbContextFactory : IDesignTimeDbContextFactory<HydraTestProjectDbContext>
    {
        public HydraTestProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HydraTestProjectDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            HydraTestProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(HydraTestProjectConsts.ConnectionStringName));

            return new HydraTestProjectDbContext(builder.Options);
        }
    }
}
