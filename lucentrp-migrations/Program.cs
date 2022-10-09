using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LucentRP.Migrations
{
    /// <summary>
    /// The main class for the lucentrp website migrator.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The appsettings.json configuration file.
        /// </summary>
        public static IConfigurationRoot AppSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();


        /// <summary>
        /// The entry point of the program.
        /// </summary>
        public static void Main()
        {
            IServiceProvider serviceProvider = CreateServices();
            using IServiceScope scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }

        /// <summary>
        /// Configure the dependency injection services.
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? AppSettings["ConnectionStrings:Default"];
            Console.WriteLine("Logging into: " + connectionString);
            Console.WriteLine("Environment Var is: " + Environment.GetEnvironmentVariable("ConnectionString"));
            
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddMySql5()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(M1_AddUserAccountsTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database.
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}