using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;

namespace LucentRP.Migrations
{
    /// <summary>
    /// The main class for the lucentrp website migrator.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        public static void Main()
        {
            // Load environment variables.
            LoadEnvironmentVariables();

            // Load the application settings.
            IConfigurationRoot appSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            // Create a service provider.
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddMySql5()
                    .WithGlobalConnectionString(appSettings["ConnectionStrings:Default"])
                    .ScanIn(typeof(M1_AddUserAccountsTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            // Run the migrations.
            using IServiceScope scope = serviceProvider.CreateScope();
            IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        /// <summary>
        /// Load envrionment variables that can be used to overwrite appsettings.json
        /// </summary>
        private static void LoadEnvironmentVariables()
        {
            // Load the file.
            string appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            string json = File.ReadAllText(appSettingsPath);

            // Deserialize the configuration.
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new ExpandoObjectConverter());
            jsonSettings.Converters.Add(new StringEnumConverter());
            dynamic? config = JsonConvert.DeserializeObject<ExpandoObject>(json, jsonSettings);

            // If no config exists.
            if (config is null)
                return;

            // Load environment variables.
            string? ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            // Overwrite the appsettings values.
            if (ConnectionString is not null)
                config.ConnectionStrings.Default = ConnectionString;

            // Serialize the configuration.
            string newJson = JsonConvert.SerializeObject(config, Formatting.Indented, jsonSettings);
            File.WriteAllText(appSettingsPath, newJson);
        }
    }
}