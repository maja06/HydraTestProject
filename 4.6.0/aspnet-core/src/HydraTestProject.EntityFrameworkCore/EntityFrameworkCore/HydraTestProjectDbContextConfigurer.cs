using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HydraTestProject.EntityFrameworkCore
{
    public static class HydraTestProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<HydraTestProjectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<HydraTestProjectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
