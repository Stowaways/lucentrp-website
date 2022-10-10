using Dapper;
using LucentRP.Features.Authentication;
using LucentRP.Features.Authorization;
using LucentRP.Features.Permissions;
using LucentRP.Features.Users;
using LucentRP.Utilities;
using MySql.Data.MySqlClient;

namespace LucentRP
{
    /// <summary>
    /// The main class for the lucentrp backend.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args">Arguments passed in through the command line.</param>
        public static void Main(string[] args)
        {
            // Create the web application builder.
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            EnvironmentConfiguration.Load();

            // Load application settings.
            IConfigurationRoot appSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            // Add services to the application.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<ILogger, Logger<Program>>();
            builder.Services.AddSingleton(appSettings);
            builder.Services.AddSingleton(new MySqlConnection(
                appSettings["ConnectionStrings:Default"]
            ));

            AuthenticationService.Register(builder.Services);
            PermissionService.Register(builder.Services);
            UserService.Register(builder.Services);

            // Build the web application.
            WebApplication app = builder.Build();

            // Connect to the database.
            MySqlConnection connection = app.Services.GetRequiredService<MySqlConnection>();
            connection.Open();

            // Configure the database connection.
            MySqlTransaction transaction = connection.BeginTransaction();
            connection.Execute("SET NAMES UTF8MB4;", null, transaction);
            connection.Execute("SET CHARACTER SET UTF8MB4;", null, transaction);
            connection.Execute("SET character_set_connection = UTF8MB4;", null, transaction);
            transaction.Commit();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            // Add routes.
            app.UseStaticFiles();
            app.UseRouting();

            // Add authentication verification, and authorization middleware.
            app.Use(new AuthenticationMiddleware(app.Services.GetRequiredService<IAuthenticate>()).Execute);
            app.Use(new AntiForgeryVerificationMiddleware(app.Services.GetRequiredService<IVerifyAntiForgeryToken>()).Execute);
            app.Use(new AuthorizationMiddleware(app.Services.GetRequiredService<PermissionAssignmentManager>()).Execute);

            app.MapControllerRoute(
                name: "default",
                pattern: "api/v1/{controller}/{action=Index}/{id?}"
            );

            // Add a default file.
            app.MapFallbackToFile("index.html");

            // Start the server.
            app.Run();
        }
    }
}