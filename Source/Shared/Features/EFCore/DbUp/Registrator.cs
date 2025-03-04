﻿using DbUp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.EFCore.Configuration;
using Shared.Features.Misc.ExecutionContext;
using System.Reflection;

namespace Shared.Features.EFCore.DbUp
{
    public static class Registrator
    {
        public static async Task<IServiceCollection> AddDbUpMigration(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var isProduction = serviceProvider.GetRequiredService<IExecutionContext>().HostingEnvironment.IsProduction();
            var efCoreConfiguration = serviceProvider.GetRequiredService<EFCoreConfiguration>();
            var connectionString = isProduction ? efCoreConfiguration.SQLServerConnectionString_Prod : efCoreConfiguration.SQLServerConnectionString_Dev;

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                               .SqlDatabase(connectionString)
                               .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                               .JournalToSqlTable("dbo", "MigrationHistory_DbUP")
                               .LogToConsole()
                               .Build();

            var scripts = upgrader.GetScriptsToExecute();
            foreach (var script in scripts)
            {
                Console.WriteLine(script.Name);
            }

            upgrader.PerformUpgrade();

            return services;
        }
    }
}
