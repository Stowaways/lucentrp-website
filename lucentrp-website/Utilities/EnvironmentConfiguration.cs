using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;

namespace LucentRP.Utilities
{
    /// <summary>
    /// A static class to overwrite appsettings.json values with
    /// environment variables if they are defined.
    /// </summary>
    public static class EnvironmentConfiguration
    {
        /// <summary>
        /// Load the variables.
        /// </summary>
        public static void Load()
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
            string? AllowedHosts = Environment.GetEnvironmentVariable("ALLOWED_HOSTS");
            string? ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            // Overwrite the appsettings values.
            if (AllowedHosts is not null)
                config.AllowedHosts = AllowedHosts;

            if (ConnectionString is not null)
                config.ConnectionStrings.Default = ConnectionString;

            // Serialize the configuration.
            string newJson = JsonConvert.SerializeObject(config, Formatting.Indented, jsonSettings);
            File.WriteAllText(appSettingsPath, newJson);
        }
    }
}
