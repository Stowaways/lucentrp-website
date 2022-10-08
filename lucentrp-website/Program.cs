using LucentRP.Features.Authentication;
using LucentRP.Features.Permissions;
using LucentRP.Features.Users;
using MySql.Data.MySqlClient;

namespace LucentRP
{
    /// <summary>
    /// The main class for the lucentrp backend.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The appsettings.json configuration file.
        /// </summary>
        public static IConfigurationRoot AppSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args">Arguments passed in through the command line.</param>
        public static void Main(string[] args)
        {
            // Create the web application builder.
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the application.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<ILogger, Logger<Program>>();
            builder.Services.AddSingleton(_ => AppSettings);
            builder.Services.AddSingleton(_ => new MySqlConnection(AppSettings["ConnectionStrings:Default"]));

            AuthenticationService.Register(builder.Services);
            PermissionService.Register(builder.Services);
            UserService.Register(builder.Services);

            // Build the web application.
            WebApplication app = builder.Build();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            // Add routes.
            app.UseStaticFiles();
            app.UseRouting();

            // Add authentication and verification middleware.
            app.Use(new AuthenticationMiddleware(app.Services.GetRequiredService<IAuthenticate>()).Execute);
            app.Use(new AntiForgeryVerificationMiddleware(app.Services.GetRequiredService<IVerifyAntiForgeryToken>()).Execute);

            app.MapControllerRoute(
                name: "default",
                pattern: "api/v1/{controller}/{action=Index}/{id?}");

            // Add a default file.
            app.MapFallbackToFile("index.html");

            // Start the server.
            app.Run();
        }
    }
}